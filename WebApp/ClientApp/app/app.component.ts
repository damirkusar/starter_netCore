import { Component, OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';
import { LoaderService } from './core/services/loader.service';
import { LoggerService } from './core/services/logger.service';

@Component({
    selector: 'app',
    template: require('./app.component.html'),
    styles: ['./app.component.scss']
})
export class AppComponent extends Localization implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy {
    showModal: boolean;

    constructor(
        private logger: LoggerService,
        private loaderService: LoaderService,
        public locale: LocaleService,
        public translation: TranslationService) {
        super(locale, translation);

        this.locale.addConfiguration()
            .addLanguages(['de', 'en'])
            .defineDefaultLocale('de', 'CH')
            .defineCurrency('CHF')
            .setCookieExpiration(30);
        this.locale.init();

        this.translation.addConfiguration().addWebAPIProvider('/api/localisations/json/', 'json');
        this.translation.translationError.subscribe((error: any) => console.log(error));
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
