import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';
import { LoggerService } from '../../../shared/services/logger.service';

@Component({
    selector: 'navmenu-elements-notification',
    template: require('./navmenu-elements-notification.component.html'),
    styles: [require('./navmenu-elements-notification.component.scss')]

})
export class NavMenuElementsNotificationComponent extends Localization implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(
        private logger: LoggerService,
        public locale: LocaleService,
        public translation: TranslationService) {
        super(locale, translation);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }
}