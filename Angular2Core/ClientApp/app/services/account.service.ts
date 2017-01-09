import { Injectable, OnChanges, OnInit, DoCheck, OnDestroy } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import './rxjs-operators';
import { ICredentials, Credentials } from '../models/credentials';
import { IUser, User } from '../models/user';
import { IToken, Token } from '../models/token';
import { HttpOptionsService } from './httpOptions.service';
import { AuthService } from './auth.service';
import { HttpErrorHandlerService } from './httpErrorHandler.service';
import { LoggerService } from './logger.service';
import { LocalStorageService } from 'angular-2-local-storage';

@Injectable()
export class AccountService implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(private _logger: LoggerService, private _httpErrorHandlerService: HttpErrorHandlerService, private _httpOptions: HttpOptionsService, private _localStorage: LocalStorageService, private _http: Http) {
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {}

    ngDoCheck(): void { }

    ngOnDestroy(): void { }
    
    getUserInfo(): Observable<IUser> {
        return this._http.get(`/api/account/userinfo`, this._httpOptions.getDefaultOptions())
            .map(response => this.extractData(response))
            .catch(error => this._httpErrorHandlerService.responseError(error));
    }

    getLoggedInUser(): IUser {
       return this._localStorage.get('loggedInUser');
    }

    private extractData(res: Response) {
        let body:IUser = res.json();
        this._localStorage.set('loggedInUser', body);
        return body || {};
    }
}