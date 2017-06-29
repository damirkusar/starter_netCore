import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { LoggerService } from './logger.service';
import { IErrorMessage } from "../../shared/models/models";

@Injectable()
export class HttpErrorHandlerService {
    errorOccured = new EventEmitter();
    constructor(private logger: LoggerService) { }

    responseError(errorResponse: Response | any) {
        let errMsg: IErrorMessage;
        if (errorResponse instanceof Response) {
            errMsg = { status: errorResponse.status, statusText: errorResponse.statusText };

            if (errorResponse['_body'] != null) {
                try {
                    var body = JSON.parse(errorResponse['_body']);
                    if (body.message != null) {
                        errMsg.message = body.message;
                    }

                    if (body.error != null) {
                        errMsg.error = body.error;
                    }

                    if (body.error_description != null) {
                        errMsg.error_description = body.error_description;
                    }
                } catch (e) {

                }

            }
        } else {
            errMsg = {
                status: errorResponse['status'],
                statusText: errorResponse['statusText'],
                error: errorResponse['error'],
                error_description: errorResponse['error_description']
            };
        }

        this.errorOccured.emit(errMsg);
        this.logger.error(`Response Error:`, errMsg);
        return Observable.throw(errMsg);
    }
}