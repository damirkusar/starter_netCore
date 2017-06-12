import { Injectable, OnChanges, OnInit, DoCheck, OnDestroy } from '@angular/core';
import { Http, Response, Headers, RequestOptions, ResponseContentType } from '@angular/http';
import { LocalStorageService } from 'angular-2-local-storage';
import { Observable } from 'rxjs/Observable';
import { LoggerService } from './logger.service';

@Injectable()
export class HttpOptionsService implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(private logger: LoggerService, private localStorageService: LocalStorageService, private http: Http) {}

    ngOnChanges(changes: Object): void {}

    ngOnInit(): void {}

    ngDoCheck(): void {}

    ngOnDestroy(): void {}

    getDefaultOptions(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization':  this.localStorageService.get('token')}); 
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    getBlobOptions(): RequestOptions {
        let options = this.getDefaultOptions();
        options.responseType = ResponseContentType.Blob;
        return options;
    }
}