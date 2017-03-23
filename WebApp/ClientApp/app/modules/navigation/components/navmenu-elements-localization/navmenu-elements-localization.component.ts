import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';

import { IUser } from '../../../shared/models/user';
import { ICredentials } from '../../../shared/models/credentials';
import { LoggerService } from '../../../shared/services/logger.service';

@Component({
    selector: 'navmenu-elements-localization',
    template: require('./navmenu-elements-localization.component.html'),
    styles: [require('./navmenu-elements-localization.component.scss')]
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