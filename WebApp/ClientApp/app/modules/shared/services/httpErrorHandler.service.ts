import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import './rxjs-operators';
import { LoggerService } from './logger.service';
import { IErrorMessage } from "../models/errorMessage";

@Injectable()
export class HttpErrorHandlerService {
    errorOccured = new EventEmitter();
    constructor(private logger: LoggerService) { }

    responseError(errorResponse: Response | any) {
        let errMsg: IErrorMessage;
        if (errorResponse instanceof Response) {
            errMsg = {status: errorResponse.status, message: errorResponse.statusText};
        } else {
            errMsg = {
                status: errorResponse.status ? errorResponse.status : -1,
                message: errorResponse.message ? errorResponse.message : errorResponse.toString()
            };
        }

        this.errorOccured.emit(errMsg);
        this.logger.error(`Response Error:`, errMsg);
        return Observable.throw(errMsg);
    }
}