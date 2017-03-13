import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { LocalStorageService } from 'angular-2-local-storage';
import { Observable } from 'rxjs/Observable';
import './rxjs-operators';
import { LoggerService } from './logger.service';

@Injectable()
export class HttpOptionsService {
    constructor(private logger: LoggerService, private localStorage: LocalStorageService, private http: Http) {}

    getDefaultOptions(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization':  this.localStorage.get('token')}); 
        let options = new RequestOptions({ headers: headers });
        return options;
    }
}