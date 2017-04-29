import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
//import { TimerObservable } from "rxjs/observable/TimerObservable";
//import { Observable } from 'rxjs/Rx';
import { IErrorMessage } from '../../shared/models/errorMessage';
import { ICredentials } from '../../shared/models/credentials';
import { IUser } from '../../shared/models/user';
import { IToken } from '../../shared/models/token';
import { HttpErrorHandlerService } from './httpErrorHandler.service';
import { LoggerService } from './logger.service';
import { LocalStorageService } from 'angular-2-local-storage';

@Injectable()
export class AuthService {
    loggedInUpdated = new EventEmitter();
    logoutTimerObservable: any;

    constructor(private logger: LoggerService, private httpErrorHandlerService: HttpErrorHandlerService, private localStorageService: LocalStorageService, private http: Http, private router: Router) {
        this.httpErrorHandlerService.errorOccured.subscribe(
            (errorMessage) => {
                this.handleHttpErrors(errorMessage);
            }
        );
    }

    handleHttpErrors(error: IErrorMessage) {
        switch (error.status) {
            case 401:
                this.logout();
                break;
        default:
        }
    }

    startLogoutTimer() {
        let tokenExpiresMillis = <number>this.localStorageService.get('token-expires-millis');
        let tokenExpiresDate: Date = new Date(tokenExpiresMillis);

        if ((this.logoutTimerObservable == null || this.logoutTimerObservable.closed) && tokenExpiresMillis != null) {
            this.setLogoutTimer(tokenExpiresDate);
        }
    }

    private setLogoutTimer(date: Date) {
        this.logger.debug(`LogoutTimer set to: ${date}`);
        var source = Observable.timer(date).timeInterval();

        this.logoutTimerObservable = source.subscribe(
            (x) => this.logger.debug('LogoutTimer fired...'),
            (error) => this.logger.error('Error: ' + error),
            () => this.logout());
    }

    login(credentials: ICredentials): Observable<IToken> {
        //this._logger.debug(`Login in... ${JSON.stringify(credentials)}`);
        this.logger.debug(`Login in...`);

        credentials.grant_type = 'password';
        credentials.scope = 'openid';
        var params = `grant_type=${credentials.grant_type}&scope=${credentials.scope}&username=${credentials.username}&password=${credentials.password}`;
        //var params = `grant_type=${credentials.grant_type}&scope=${credentials.scope}&username=damir@kusar.ch&password=Password1$`; 

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(`/connect/token`, params, options)
            .map(response => this.extractSuccessData(response as Response))
            .catch(error => this.httpErrorHandlerService.responseError(error));
    }

    logout() {
        this.logger.debug("Loging out...");
        this.router.navigate(['/home']);
        this.localStorageService.set('loggedIn', undefined);
        this.localStorageService.set('token', undefined);
        this.localStorageService.set('token-expires-millis', undefined);
        this.localStorageService.set('loggedInUser', undefined); // AccountService
        this.logoutTimerObservable.unsubscribe();
        this.logoutTimerObservable = undefined;
        this.loggedInUpdated.emit(false);
    }

    isLoggedIn(): boolean {
        return this.localStorageService.get('loggedIn') === true;
    }

    getCurrentUser(): IUser {
        return this.localStorageService.get('loggedInUser');
    }

    isInRole(allowedRoles: string[]): boolean {
        let loggedInUser: IUser = this.getCurrentUser();
        if (loggedInUser && loggedInUser.assignedRoles) {
            let userRoles = loggedInUser.assignedRoles;
            let userRolesMapped = userRoles.map(userRole => userRole.toLowerCase());

            let isInRole = allowedRoles.some(allowedRole => userRolesMapped.indexOf(allowedRole.toLowerCase()) >= 0);

            return isInRole;
        }
        return false;
    }

    private extractSuccessData(res: Response) { 
        let token: IToken = res.json();
        this.localStorageService.set('loggedIn', true);
        this.localStorageService.set('token', `${token.token_type} ${token.access_token}`);

        let date = new Date();
        date.setSeconds(date.getSeconds() + token.expires_in);
        date.setMinutes(date.getMinutes() - 1);
        let time = date.getTime();
        this.localStorageService.set('token-expires-millis', time);
        this.startLogoutTimer();

        this.loggedInUpdated.emit(true);

        return token || {};
    }

}