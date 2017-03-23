import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';
import { ICredentials } from '../../../shared/models/credentials';
import { IUser } from '../../../shared/models/user';
import { LoggerService } from '../../../shared/services/logger.service';

@Component({
    selector: 'navmenu-top',
    template: require('./navmenu-top.component.html'),
    styles: [require('./navmenu-top.component.scss')]
})
export class NavMenuTopComponent extends Localization implements OnChanges, OnInit, DoCheck, OnDestroy {
    private loggedIn: boolean;
    @Input() user: IUser;
    @Input() loginError: boolean;
    @Output() onLogin = new EventEmitter<ICredentials>();
    @Output() onLogout = new EventEmitter();

    constructor(private logger: LoggerService,
        public locale: LocaleService,
        public translation: TranslationService) {
        super(locale, translation);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }

    @Input()
    set isLoggedIn(isLoggedIn: boolean) {
        this.loggedIn = isLoggedIn;
    }

    get isLoggedIn(): boolean {
        return this.loggedIn;
    }

    onLoginPerformed(credentials: ICredentials) {
        //this._logger.debug(`OnLoginEvent Received with credentials in navmenu-top: ${credentials.username} & ${credentials.password}`);
        this.onLogin.emit(credentials);
    }

    onLogoutPerformed(event: Event) {
        //this._logger.debug(`OnLogoutEvent Received in navmenu-top`);
        this.onLogout.emit();
    }
}