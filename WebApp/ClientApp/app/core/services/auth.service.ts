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

        this.accountService.loggedInUserUpdated.subscribe((user: IUser) => {
            if (user) {
                this.startLogoutTimer();
                this.updateLoggedIn(true);
                this.loaderService.setShowModal(false);
            }
        });
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

    init() {
        //this.loadCurrentUser();
    }

    private setLogoutTimer(logoutInSeconds?: number) {
        logoutInSeconds = logoutInSeconds || 86400; // 86400 seconds are 24 hours

        let date = new Date();

        date.setSeconds(date.getSeconds() + logoutInSeconds - 30);
        let logoutTimeInMillis = date.getTime();
        this.localStorageService.set('logoutTimeInMillis', logoutTimeInMillis);
    }

    private startLogoutTimer() {
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

    private loadCurrentUser() {
        this.accountService.getUserInfoAfterLogin().subscribe(x => { }, error => this.logger.error(error));
    }

    isInRole(allowedRoles: string[]): boolean {
        let loggedInUser: IUser = this.accountService.getLoggedInUser();
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

    login(credentials: ICredentials): void {
        this.logger.debug(`Logging in...`);
        this.loaderService.setShowModal(true);

        credentials.grant_type = 'password';
        credentials.scope = 'openid';
        var params = `grant_type=${credentials.grant_type}&scope=${credentials.scope}&username=${credentials.username}&password=${credentials.password}`;
        this.logger.log(`Logging in...`, params);

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let options = new RequestOptions({ headers: headers });

        var login = this.http.post(`${authApi}/token`, params, options)
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                this.loaderService.setShowModal(false);
                return this.httpErrorHandlerService.responseError(error);
            });

        login.subscribe((response: Response) => {
            this.updateLoggedIn(true);

            let token: IToken = response.json();
            this.localStorageService.set('token', `${token.token_type} ${token.access_token}`);

            this.setLogoutTimer(token.expires_in);
            this.startLogoutTimer();
            this.loadCurrentUser();
        },
            error => {
                this.loaderService.setShowModal(false);
                return this.httpErrorHandlerService.responseError(error);
            });
    }

    logout() {
        this.logger.debug("Logging out...");
        this.updateLoggedIn(false);
        this.accountService.logoutCurrentUser(); // AccountService
        this.localStorageService.set('token', undefined);
        this.localStorageService.set('logoutTimeInMillis', undefined);

        if (this.logoutTimerObservable) {
            this.logoutTimerObservable.unsubscribe();
            this.logoutTimerObservable = undefined;
        }

        this.router.navigate(['/dashboard']);

        this.logger.debug("Logged out...");
    }

    //loginCookie(credentials: ICredentials): void {
    //    this.logger.debug(`Logging in...`);
    //    this.loaderService.setShowModal(true);

    //    var login = this.http.post(`${authApi}/login`, { email: credentials.username, password: credentials.password }, this.httpOptionsService.getDefaultOptions())
    //        .map((response: Response) => {
    //            return response;
    //        })
    //        .catch(error => {
    //            this.loaderService.setShowModal(false);
    //            return this.httpErrorHandlerService.responseError(error);
    //        });


    //    login.subscribe((response: Response) => {
    //        this.setLogoutTimer();
    //        //this.loadCurrentUser();

    //        let user: IUser = response.json();
    //        this.localStorageService.set('loggedInUser', user);
    //        this.accountService.loggedInUserUpdated.emit(user);
    //    },
    //        error => {
    //            this.loaderService.setShowModal(false);
    //            return this.httpErrorHandlerService.responseError(error);
    //        });
    //}

    //logoutCookie() {
    //    this.updateLoggedIn(false);
    //    this.localStorageService.set('logoutTimeInMillis', undefined);

    //    this.accountService.logoutCurrentUser(); // AccountService

    //    if (this.logoutTimerObservable) {
    //        this.logoutTimerObservable.unsubscribe();
    //        this.logoutTimerObservable = undefined;
    //    }

    //    var logout = this.http.post(`${authApi}/logout`, {}, this.httpOptionsService.getDefaultOptions())
    //        .map(response => { this.logger.log('logout', response) })
    //        .catch(error => {
    //            return this.httpErrorHandlerService.responseError(error);
    //        });

    //    logout.subscribe();
    //    this.router.navigate(['/dashboard']);
    //    this.logger.debug("Logged out...");
    //}
}