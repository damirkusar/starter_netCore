import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';

import { LoggerService } from '../../../core/services/logger.service';

@Component({
    selector: 'navmenu-elements-localization',
    templateUrl: './navmenu-elements-localization.component.html',
    styleUrls: ['./navmenu-elements-localization.component.scss']
})
export class NavMenuElementsLocalizationComponent extends Localization implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(private logger: LoggerService,
        public translation: TranslationService, public locale: LocaleService) { 
        super(locale, translation);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }

    selectLocale(language: string, country: string, currency: string): void {
        this.locale.setDefaultLocale(language, country);
        this.locale.setCurrentCurrency(currency);
    }
}