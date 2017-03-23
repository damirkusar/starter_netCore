import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';
import { LoggerService } from '../../shared/services/logger.service';

@Component({
    selector: 'admin',
    template: require('./admin.component.html'),
    styles: [require('./admin.component.scss')]
})
export class AdminComponent extends Localization implements OnChanges, OnInit, DoCheck, OnDestroy {
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
        this.logger.debug(`AdminComponent-canDeactivate`);
        return true;
    }
}