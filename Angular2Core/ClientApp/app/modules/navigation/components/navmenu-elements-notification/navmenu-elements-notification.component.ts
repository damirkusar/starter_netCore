import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Locale, LocaleService, LocalizationService } from 'angular2localization';
import { LoggerService } from '../../../../services/logger.service';

@Component({
    selector: 'navmenu-elements-notification',
    template: require('./navmenu-elements-notification.component.html'),
    styles: [require('./navmenu-elements-notification.component.scss')]

})
export class NavMenuElementsNotificationComponent extends Locale implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(
        private logger: LoggerService,
        public locale: LocaleService,
        public localization: LocalizationService) {
        super(locale, localization);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }
}