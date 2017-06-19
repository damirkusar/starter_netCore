import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { IErrorMessage, ICredentials, IUser, IToken } from '../../shared/models/models';
import { AccountService } from './account.service';
import { HttpOptionsService } from './http-options.service';
import { HttpErrorHandlerService } from './http-error-handler.service';
import { LoaderService } from './loader.service';
import { LoggerService } from './logger.service';
import { LocalStorageService } from 'angular-2-local-storage';

const authApi: string = '/api/auth';

@Injectable()
export class AuthService {
    loggedInUserUpdated: EventEmitter<IUser> = new EventEmitter<IUser>();
    loggedInUpdated = new EventEmitter();
    logoutTimerObservable: any;

    constructor(
        private logger: LoggerService,
        private loaderService: LoaderService,
        private httpErrorHandlerService: HttpErrorHandlerService,
        private localStorageService: LocalStorageService,
        private http: Http,
        private router: Router,
        private httpOptionsService: HttpOptionsService,
        private accountService: AccountService) {

        this.httpErrorHandlerService.errorOccured.subscribe(
            (errorMessage) => {
                this.handleHttpErrors(errorMessage);
            }
        );
    }

    private handleHttpErrors(error: IErrorMessage) {
        switch (error.status) {
            case 401:
                this.logger.error("401", error);
                this.logout();
                break;
            default:
        }
    }

    private startLogoutTimer(logoutInHours?: number) {
        let logoutTimeInMillis = this.getLogoutTimeInMillis();
        if (logoutTimeInMillis == null) {
            this.setLogoutTimer();
            logoutTimeInMillis = this.getLogoutTimeInMillis();
        }
        let logoutTimeDate: Date = new Date(logoutTimeInMillis);

        if ((this.logoutTimerObservable == null || this.logoutTimerObservable.closed) && logoutTimeInMillis != null) {
            this.activateLogoutTimer(logoutTimeDate);
        }
    }

    private setLogoutTimer(logoutInHours?: number) {
        logoutInHours = logoutInHours || 24;

        let date = new Date();

        date.setHours(date.getHours() + logoutInHours);
        let logoutTimeInMillis = date.getTime();
        this.localStorageService.set('logoutTimeInMillis', logoutTimeInMillis);
    }

    private activateLogoutTimer(date: Date) {
        this.logger.debug(`LogoutTimer set to: ${date}`);
        var timer = Observable.timer(date);

        this.logoutTimerObservable = timer.subscribe(
            (x) => {
                this.logger.debug('LogoutTimer fired...');
                this.logout();
            });
    }

    private getLogoutTimeInMillis(): number {
        return <number>this.localStorageService.get('logoutTimeInMillis');
    }

    private updateLoggedIn(loggedIn) {
        this.localStorageService.set('loggedIn', loggedIn);
        this.loggedInUpdated.emit(loggedIn);
    }

    isInRole(allowedRoles: string[]): boolean {
        let loggedInUser: IUser = this.getLoggedInUser();
        if (loggedInUser && loggedInUser.assignedRoles) {
            let userRoles = loggedInUser.assignedRoles;
            let userRolesMapped = userRoles.map(userRole => userRole.toLowerCase());

            let isInRole = allowedRoles.some(allowedRole => userRolesMapped.indexOf(allowedRole.toLowerCase()) >= 0);

            return isInRole;
        }
        return false;
    }

    isLoggedIn(): boolean {
        return this.localStorageService.get('loggedIn') === true;
    }

    login(credentials: ICredentials) {
        this.logger.debug(`Loggin in...`);
        this.loaderService.setShowModal(true);

        var login = this.http.post(`${authApi}/login`, { userName: credentials.userName, password: credentials.password }, this.httpOptionsService.getDefaultOptions())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                this.loaderService.setShowModal(false);
                return this.httpErrorHandlerService.responseError(error);
            });


        login.subscribe((response: Response) => {
            var currentUser: IUser = response.json();
            this.localStorageService.set('loggedInUser', currentUser);
            this.loggedInUserUpdated.emit(currentUser);
            this.updateLoggedIn(true);
            this.setLogoutTimer();
            this.startLogoutTimer();
            this.loaderService.setShowModal(false);
        },
            error => {
                this.loaderService.setShowModal(false);
                return this.httpErrorHandlerService.responseError(error);
            });
    }

    logout() {
        this.updateLoggedIn(false);
        this.localStorageService.set('logoutTimeInMillis', undefined);

        this.logoutCurrentUser();

        if (this.logoutTimerObservable) {
            this.logoutTimerObservable.unsubscribe();
            this.logoutTimerObservable = undefined;
        }

        var logout = this.http.post(`${authApi}/logout`, {}, this.httpOptionsService.getDefaultOptions())
            .map(response => { this.logger.log('logout', response) })
            .catch(error => {
                return this.httpErrorHandlerService.responseError(error);
            });

        logout.subscribe();
        this.router.navigate(['/home']);
        this.logger.debug("Logged out...");
    }

    logoutCurrentUser() {
        this.localStorageService.set('loggedInUser', undefined);
        this.loggedInUserUpdated.emit(undefined);
    }

    getLoggedInUser(): IUser {
        return this.localStorageService.get('loggedInUser');
    }

    //logoutToken() {
    //    this.logger.debug("Loging out...");
    //    this.router.navigate(['/home']);
    //    this.localStorageService.set('loggedIn', undefined);
    //    this.localStorageService.set('token', undefined);
    //    this.localStorageService.set('logoutTimeInMillis', undefined);
    //    this.localStorageService.set('loggedInUser', undefined); // AccountService
    //    this.logoutTimerObservable.unsubscribe();
    //    this.logoutTimerObservable = undefined;
    //    this.loggedInUpdated.emit(false);
    //}

    //loginToken(credentials: ICredentials): Observable<IToken> {
    //    this.logger.debug(`Login in...`);

    //    credentials.grant_type = 'password';
    //    credentials.scope = 'openid';
    //    var params = `grant_type=${credentials.grant_type}&scope=${credentials.scope}&username=${credentials.username}&password=${credentials.password}`;

    //    let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
    //    let options = new RequestOptions({ headers: headers });

    //    return this.http.post(`${authApi}/token`, params, options)
    //        .map(response => this.extractSuccessTokenData(response as Response))
    //        .catch(error => {
    //            return this.httpErrorHandlerService.responseError(error);
    //        });
    //}

    //extractSuccessTokenData(res: Response) {
    //    let token: IToken = res.json();
    //    this.localStorageService.set('loggedIn', true);
    //    this.localStorageService.set('token', `${token.token_type} ${token.access_token}`);

    //    let date = new Date();
    //    date.setSeconds(date.getSeconds() + token.expires_in);
    //    date.setMinutes(date.getMinutes() - 1);
    //    let time = date.getTime();
    //    this.localStorageService.set('logoutTimeInMillis', time);
    //    this.startLogoutTimer();

    //    this.loggedInUpdated.emit(true);

    //    return token || {};
    //}
}