import { Component, OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Locale, LocaleService, LocalizationService } from 'angular2localization';
import { LoaderService } from '../../services/loader.service';
import { LoggerService } from '../../services/logger.service';

@Component({
    selector: 'app',
    template: require('./app.component.html'),
    styles: [require('./app.component.scss')]
})
export class AppComponent extends Locale implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy {
    showModal: boolean;

    constructor(
        private logger: LoggerService,
        private loaderService: LoaderService,
        public locale: LocaleService,
        public localization: LocalizationService) {

        super(locale, localization);

        this.locale.addLanguages(['de', 'en']);

        // Required: default language, country (ISO 3166 two-letter, uppercase code) and expiry (No days). If the expiry is omitted, the cookie becomes a session cookie.
        // Selects the default language and country, regardless of the browser language, to avoid inconsistencies between the language and country.
        this.locale.definePreferredLocale('de', 'CH', 30);

        // Optional: default currency (ISO 4217 three-letter code).
        this.locale.definePreferredCurrency('CHF');

        // Initializes LocalizationService: asynchronous loading.
        this.localization.translationProvider('./resources/locale-'); // Required: initializes the translation provider with the given path prefix.
        localization.setMissingKey("missing");
        this.localization.updateTranslation(); // Need to update the translation.
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
