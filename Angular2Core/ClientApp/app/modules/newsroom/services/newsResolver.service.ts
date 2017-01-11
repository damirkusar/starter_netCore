import { Injectable } from '@angular/core';
import { Router, Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import '../../../services/rxjs-operators';
import { LoggerService } from '../../../services/logger.service';
import { LoaderService } from '../../../services/loader.service';
import { INews, News, NewsService } from './news.service';

@Injectable()
export class NewsResolverService implements Resolve<INews> {
  constructor(private _logger: LoggerService, private _loaderService: LoaderService, private _newsService: NewsService, private _router: Router) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<INews[]> | any {
    //this._loaderService.setShowModal(true);
    let params = route.params;
    
    //this._logger.debug(`NewsResolverService route ${JSON.stringify(params)}`, route);
    //this._logger.debug(`NewsResolverService state ${JSON.stringify(params)}`, route);

      return this._newsService.getNews()
          .map(news => {
              //this._logger.debug(`NewsResolverService News: ${JSON.stringify(news)}`);
              //this._loaderService.setShowModal(false);
              return news;
          }) 
          .catch(error => {
              this._logger.error(`NewsResolverService Error: ${JSON.stringify(error)}`);
              // this._loaderService.setShowModal(false);
              // this._router.navigate(['/home']); 
              return Observable.throw(error);
          });
  }
}


