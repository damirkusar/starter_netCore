import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';
import { LoggerService } from '../../core/services/logger.service';

@Component({
    selector: 'contact',
    template: require('./contact.component.html'),
    styles: [require('./contact.component.scss')]
})
export class ContactComponent extends Localization implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(private logger: LoggerService,
        public locale: LocaleService,
        public translation: TranslationService) {
        super(locale, translation);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }

    canDeactivate(): Promise<boolean> | boolean {
        this.logger.debug(`ContactComponent-canDeactivate`);
        return true;
    }
}