import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { ICredentials } from '../../shared/models/credentials';
import { IUser } from '../../shared/models/user';
import { IToken } from '../../shared/models/token';
import { HttpOptionsService } from './httpOptions.service';
import { AuthService } from './auth.service';
import { HttpErrorHandlerService } from './httpErrorHandler.service';
import { LoggerService } from './logger.service';
import { LocalStorageService } from 'angular-2-local-storage';

const accountApi: string = '/api/account';

@Injectable()
export class AccountService {
    loggedInUserUpdated: EventEmitter<IUser> = new EventEmitter<IUser>();

    constructor(private logger: LoggerService, private httpErrorHandlerService: HttpErrorHandlerService, private httpOptions: HttpOptionsService, private localStorageService: LocalStorageService, private http: Http) {
    }
    
    getUserInfo(): Observable<IUser> {
        return this.http.get(`${accountApi}/userinfo`, this.httpOptions.getDefaultOptions())
            .map(response => this.extractData(response as Response))
            .catch(error => this.httpErrorHandlerService.responseError(error));
    }

    getLoggedInUser(): IUser {
        return this.localStorageService.get('loggedInUser');
    }

    private extractData(res: Response) {
        let body:IUser = res.json();
        this.localStorageService.set('loggedInUser', body);
        this.loggedInUserUpdated.emit(body);
        return body || {};
    }
}