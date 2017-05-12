import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';

import { Router, Resolve, RouterStateSnapshot, ActivatedRoute } from '@angular/router';
import { LoggerService } from '../../core/services/logger.service';
import { INews } from './services/news.service';

@Component({
    selector: 'news-room',
    template: require('./newsroom.component.html'),
    styles: [require('./newsroom.component.scss')]
})
export class NewsRoomComponent extends Localization implements OnChanges, OnInit, DoCheck, OnDestroy {
    news: INews;
    error: any;
    constructor(
        private logger: LoggerService,
        private route: ActivatedRoute,
        public locale: LocaleService,
        public translation: TranslationService) {
        super(locale, translation);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {
        this.route.data.subscribe(
            (data: { news: INews }) => {
                this.news = data.news;
            }, error => this.error = error);
    }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }

    canDeactivate(): Promise<boolean> | boolean {
        this.logger.debug(`NewsRoomComponent-canDeactivate`);
        return true;
    }
}
