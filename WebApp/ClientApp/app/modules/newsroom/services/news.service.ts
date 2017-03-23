import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import '../../shared/services/rxjs-operators';
import { LoggerService } from '../../shared/services/logger.service';
import { HttpErrorHandlerService } from '../../shared/services/httpErrorHandler.service';
import { HttpOptionsService } from '../../shared/services/httpOptions.service';

export interface INews {
    id?: string;
    title?: string;
    content?:string;
}

@Injectable()
export class NewsService {
    private newsUrl = './news.json';
    constructor(private logger: LoggerService, 
        private httpErrorHandlerService: HttpErrorHandlerService,
        private httpOptions: HttpOptionsService,
        private http: Http) {
    }

    getNews(): Observable<INews[]> {
        return this.http.get(this.newsUrl, this.httpOptions.getDefaultOptions())
            .map(response => this.extractData(response as Response))
            .catch(error => this.httpErrorHandlerService.responseError(error));
    }

    private extractData(res: Response) {
        let body: INews = res.json();
        return body || { };
      }
}