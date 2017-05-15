import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';
import { LoggerService } from '../../../core/services/logger.service';

@Component({
    selector: 'site-header',
    template: './siteheader.component.html',
    styles: ['./siteheader.component.scss']
})
export class SiteHeaderComponent extends Localization implements OnChanges, OnInit, DoCheck, OnDestroy {
    filterValue: string;
    @Input()
    title: string;

    constructor(
        private logger: LoggerService, 
        public locale: LocaleService,
        public translation: TranslationService
        ) { 
        super(locale, translation);
    }

    @Output() filterChange = new EventEmitter();
    @Input()
    get filter() {
        return this.filterValue;
    }

    set filter(val) {
        this.filterValue = val;
        this.filterChange.emit(this.filterValue);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {
    }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }

    canDeactivate(): Promise<boolean> | boolean {
        this.logger.debug(`SiteHeaderComponent-canDeactivate`);
        return true;
    }
}
