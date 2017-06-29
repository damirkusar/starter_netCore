import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { IErrorMessage, ICredentials, IUser, IToken } from '../../shared/models/models';
import { HttpOptionsService } from './http-options.service';
import { AuthService } from './auth.service';
import { HttpErrorHandlerService } from './http-error-handler.service';
import { LoggerService } from './logger.service';
import { LocalStorageService } from 'angular-2-local-storage';

const accountApi: string = '/api/account';
const usersApi: string = '/api/users';

@Injectable()
export class AccountService {
    loggedInUserUpdated: EventEmitter<IUser> = new EventEmitter<IUser>();

    constructor(
        private logger: LoggerService,
        private httpErrorHandlerService: HttpErrorHandlerService,
        private httpOptions: HttpOptionsService,
        private localStorageService: LocalStorageService,
        private http: Http
    ) {

    }

    getUserInfoAfterLogin(): Observable<IUser> {
        return this.http.get(`${usersApi}/userinfo`, this.httpOptions.getDefaultOptions())
            .map(response => this.extractCurrentUserData(response as Response))
            .catch(error => this.httpErrorHandlerService.responseError(error));
    }

    getLoggedInUser(): IUser {
        return this.localStorageService.get('loggedInUser');
    }

    changePassword(settings): any {
        return this.http.post(`${accountApi}/changePassword`, settings, this.httpOptions.getDefaultOptions())
            .map(response => response)
            .catch(error => this.httpErrorHandlerService.responseError(error));
    }

    register(settings): any {
        return this.http.post(`${accountApi}/Register`, settings, this.httpOptions.getDefaultOptions())
            .map((response: Response) => response.json())
            .catch(error => this.httpErrorHandlerService.responseError(error));
    }

    logoutCurrentUser() {
        this.localStorageService.set('loggedInUser', undefined);
        this.loggedInUserUpdated.emit(null);
    }

    private extractCurrentUserData(res: Response) {
        let user: IUser = res.json();
        //user.fullName = `${user.firstName} ${user.lastName}`;
        this.localStorageService.set('loggedInUser', user);
        this.loggedInUserUpdated.emit(user);
        return user || {};
    }

    private extractUserData(res: Response) {
        let user: IUser = res.json();
        return user || {};
    }
}