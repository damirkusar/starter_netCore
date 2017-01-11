import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Router, Resolve, RouterStateSnapshot, ActivatedRoute } from '@angular/router';
import { LoggerService } from '../../../../services/logger.service';
import { INews, News } from '../../services/news.service';

@Component({
    selector: 'news-room',
    template: require('./newsroom.component.html'),
    styles: [require('./newsroom.component.scss')]
})
export class NewsRoomComponent implements OnChanges, OnInit, DoCheck, OnDestroy {
    news: INews;
    error: any;
    constructor(private _logger: LoggerService, private _route: ActivatedRoute) { }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {
        this._route.data.subscribe(
            (data: { news: INews }) => {
                this.news = data.news;
            }, error => this.error = error);
    }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }

    canDeactivate(): Promise<boolean> | boolean {
        // Allow synchronous navigation (`true`) if no crisis or the crisis is unchanged
        this._logger.debug(`NewsRoomComponent-canDeactivate`);
        // Otherwise ask the user with the dialog service and return its
        // promise which resolves to true or false when the user decides
        return true;
    }
}
