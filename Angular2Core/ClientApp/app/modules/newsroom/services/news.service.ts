import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import '../../../services/rxjs-operators';
import { LoggerService } from '../../../services/logger.service';
import { HttpErrorHandlerService } from '../../../services/httpErrorHandler.service';
import { HttpOptionsService } from '../../../services/httpOptions.service';

export interface INews {
    id?: string;
    title?: string;
    content?:string;
}

export class News implements INews {
    id?: string;
    title?: string;
    content?:string;

    constructor(title?: string, content?: string) {
        this.title = title;
        this.content = content;
    }
}

@Injectable()
export class NewsService {
    private _newsUrl = './news.json';
    constructor(private _logger: LoggerService, 
        private _httpErrorHandlerService: HttpErrorHandlerService,
        private _httpOptions: HttpOptionsService,
        private _http: Http) {
    }

    getNews(): Observable<INews[]> {
        return this._http.get(this._newsUrl, this._httpOptions.getDefaultOptions())
            .map(response => this.extractData(response))
            .catch(error => this._httpErrorHandlerService.responseError(error));
    }

    private extractData(res: Response) {
        let body: INews = res.json();
        return body || { };
      }
}