import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Locale, LocaleService, LocalizationService } from 'angular2localization';

import { IUser } from '../../../../models/user';
import { ICredentials } from '../../../../models/credentials';
import { LoggerService } from '../../../../services/logger.service';

@Component({
    selector: 'navmenu-elements-localization',
    template: require('./navmenu-elements-localization.component.html'),
    styles: [require('./navmenu-elements-localization.component.scss')]

})
export class NavMenuElementsLocalizationComponent extends Locale implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(private logger: LoggerService,
        public locale: LocaleService,
        public localization: LocalizationService) { 
        super(locale, localization);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }

    selectLocale(language: string, country: string, currency: string): void {
        this.locale.setCurrentLocale(language, country);
        this.locale.setCurrentCurrency(currency);
    }
}