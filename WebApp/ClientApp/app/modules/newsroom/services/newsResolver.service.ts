import { Injectable } from '@angular/core';
import { Router, Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { LoggerService } from '../../../core/services/logger.service';
import { LoaderService } from '../../../core/services/loader.service';
import { INews, NewsService } from './news.service';

@Injectable()
export class NewsResolverService implements Resolve<INews> {
    constructor(private logger: LoggerService, private loaderService: LoaderService, private newsService: NewsService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<INews[]> | any {
        //this._loaderService.setShowModal(true);
        let params = route.params;

        //this._logger.debug(`NewsResolverService route ${JSON.stringify(params)}`, route);
        //this._logger.debug(`NewsResolverService state ${JSON.stringify(params)}`, route);

        return this.newsService.getNews()
            .map(news => {
                //this._logger.debug(`NewsResolverService News: ${JSON.stringify(news)}`);
                //this._loaderService.setShowModal(false);
                return news;
            })
            .catch(error => {
                this.logger.error(`NewsResolverService Error: ${JSON.stringify(error)}`);
                // this._loaderService.setShowModal(false);
                // this._router.navigate(['/home']); 
                return Observable.throw(error);
            });
    }
}


