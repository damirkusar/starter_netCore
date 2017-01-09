import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
//import { TimerObservable } from "rxjs/observable/TimerObservable";
//import { Observable } from 'rxjs/Rx';
import './rxjs-operators';
import { ICredentials, Credentials } from '../models/credentials';
import { IErrorMessage, ErrorMessage } from '../models/errorMessage';
import { IToken, Token } from '../models/token';
import { HttpErrorHandlerService } from './httpErrorHandler.service';
import { LoggerService } from './logger.service';
import { LocalStorageService } from 'angular-2-local-storage';

@Injectable()
export class AuthService {
    loggedInUpdated = new EventEmitter();
    logoutTimerObservable: any;

    constructor(private _logger: LoggerService, private _httpErrorHandlerService: HttpErrorHandlerService, private _localStorage: LocalStorageService, private _http: Http, private _router: Router) {
        this._httpErrorHandlerService.errorOccured.subscribe(
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
        let tokenExpiresMillis = <number>this._localStorage.get('token-expires-millis');
        let tokenExpiresDate: Date = new Date(tokenExpiresMillis);

        if ((this.logoutTimerObservable == null || this.logoutTimerObservable.closed) && tokenExpiresMillis != null) {
            this.setLogoutTimer(tokenExpiresDate);
        }
    }

    private setLogoutTimer(date: Date) {
        this._logger.debug(`LogoutTimer set to: ${date}`);
        var source = Observable.timer(date).timeInterval();

        this.logoutTimerObservable = source.subscribe(
            (x) => this._logger.debug('LogoutTimer fired...'),
            (error) => this._logger.error('Error: ' + error),
            () => this.logout());
    }

    login(credentials: ICredentials): Observable<IToken> {
        //this._logger.debug(`Login in... ${JSON.stringify(credentials)}`);
        this._logger.debug(`Login in...`);

        credentials.grant_type = 'password';
        credentials.scope = 'openid';
        var params = `grant_type=${credentials.grant_type}&scope=${credentials.scope}&username=${credentials.username}&password=${credentials.password}`;
        //var params = `grant_type=${credentials.grant_type}&scope=${credentials.scope}&username=damir@kusar.ch&password=Password1$`; 

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let options = new RequestOptions({ headers: headers });

        return this._http.post(`/connect/token`, params, options)
            .map(response => this.extractSuccessData(response))
            .catch(error => this._httpErrorHandlerService.responseError(error));
    }

    logout() {
        this._logger.debug("Loging out...");
        this._router.navigate(['/home']);
        this._localStorage.set('loggedIn', undefined);
        this._localStorage.set('token', undefined);
        this._localStorage.set('token-expires-millis', undefined);
        this._localStorage.set('loggedInUser', undefined); // AccountService
        this.logoutTimerObservable.unsubscribe();
        this.logoutTimerObservable = undefined;
        this.loggedInUpdated.emit(false);
    }

    isLoggedIn(): boolean {
        return this._localStorage.get('loggedIn') === true;
    }

    private extractSuccessData(res: Response) { 
        let token: IToken = res.json();
        this._localStorage.set('loggedIn', true);
        this._localStorage.set('token', `${token.token_type} ${token.access_token}`);

        let date = new Date();
        date.setSeconds(date.getSeconds() + token.expires_in);
        date.setMinutes(date.getMinutes() - 1);
        let time = date.getTime();
        this._localStorage.set('token-expires-millis', time);
        this.startLogoutTimer();

        this.loggedInUpdated.emit(true);

        return token || {};
    }

}