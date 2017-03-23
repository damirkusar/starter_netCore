import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import './rxjs-operators';
import {IErrorMessage} from "../models/errorMessage";

@Injectable()
export class LoggerService {
    constructor() { }

    log(msg: any, params?: any) {
        if (params) {
            console.log(msg, params);
        } else {
            console.log(msg);
        }
    }

    error(msg: any, params?: any) {
        if (params) {
            console.error(msg, params);
        } else {
            console.error(msg);
        }
    }

    warn(msg: any, params?: any) {
        if (params) {
            console.warn(msg, params);
        } else {
            console.warn(msg);
        }
    }

    debug(msg: any, params?: any) {
        if (params) {
            console.debug(msg, params);
        } else {
            console.debug(msg);
        }
    }

    info(msg: any, params?: any) {
        if (params) {
            console.info(msg, params);
        } else {
            console.info(msg);
        }
    }
}