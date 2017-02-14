import { Component, OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';
import { LoaderService } from '../../services/loader.service';
import { LoggerService } from '../../services/logger.service';

@Component({
    selector: 'app',
    template: require('./app.component.html'),
    styles: [require('./app.component.scss')]
})
export class AppComponent extends Localization implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy {
    showModal: boolean;

    constructor(
        private logger: LoggerService,
        private loaderService: LoaderService,
        public locale: LocaleService,
        public translation: TranslationService) {
        super(locale, translation);

        this.locale.AddConfiguration()
            .AddLanguages(['de', 'en'])
            .DefineDefaultLocale('de', 'CH')
            .DefineCurrency('CHF')
            .SetCookieExpiration(30);
        this.locale.init();
        
        this.translation.AddConfiguration()
            .AddProvider('/api/localization/', 'json', true);
        this.translation.init();
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {
        this.loaderService.showModalUpdated.subscribe(
            (show) => {
                this.showModal = show;
            }
        );
    }

    ngDoCheck(): void { }

    ngAfterContentInit(): void { }

    ngAfterContentChecked(): void { }

    ngAfterViewInit(): void { }

    ngAfterViewChecked(): void { }

    ngOnDestroy(): void {
        this.loaderService.showModalUpdated.unsubscribe();
    }
}
