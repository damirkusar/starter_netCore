import { Injectable, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter } from '@angular/core';
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
    loggedInUserUpdated: EventEmitter<IUser> = new EventEmitter<IUser>();

    constructor(private logger: LoggerService, private httpErrorHandlerService: HttpErrorHandlerService, private httpOptions: HttpOptionsService, private localStorage: LocalStorageService, private http: Http) {
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {}

    ngDoCheck(): void { }

    ngOnDestroy(): void { }
    
    getUserInfo(): Observable<IUser> {
        return this.http.get(`/api/account/userinfo`, this.httpOptions.getDefaultOptions())
            .map(response => this.extractData(response as Response))
            .catch(error => this.httpErrorHandlerService.responseError(error));
    }

    getLoggedInUser(): IUser {
       return this.localStorage.get('loggedInUser');
    }

    private extractData(res: Response) {
        let body:IUser = res.json();
        this.localStorage.set('loggedInUser', body);
        this.loggedInUserUpdated.emit(body);
        return body || {};
    }
}