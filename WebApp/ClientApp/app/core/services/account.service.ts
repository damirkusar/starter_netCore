import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { IUser, IUserRegister } from '../../shared/models/models';
import { HttpOptionsService } from './http-options.service';
import { AuthService } from './auth.service';
import { HttpErrorHandlerService } from './http-error-handler.service';
import { LoggerService } from './logger.service';
import { LocalStorageService } from 'angular-2-local-storage';

const accountApi: string = '/api/account';

@Injectable()
export class AccountService {

    constructor(private logger: LoggerService, private httpErrorHandlerService: HttpErrorHandlerService, private httpOptions: HttpOptionsService, private localStorageService: LocalStorageService, private http: Http) {
    }

    registerw(settings: IUserRegister): any {
        return this.http.post(`${accountApi}/Register`, settings, this.httpOptions.getDefaultOptions())
            .map((response: Response) => response.json())
            .catch(error => this.httpErrorHandlerService.responseError(error));
    }
}