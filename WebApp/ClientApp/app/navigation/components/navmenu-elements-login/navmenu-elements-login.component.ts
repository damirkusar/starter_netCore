import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';
import { IUser } from '../../../shared/models/user';
import { ICredentials } from '../../../shared/models/credentials';
import { LoggerService } from '../../../core/services/logger.service';

@Component({
    selector: 'navmenu-elements-login',
    template: require('./navmenu-elements-login.component.html'),
    styles: [require('./navmenu-elements-login.component.scss')]
})
export class NavMenuElementsLoginComponent extends Localization implements OnChanges, OnInit, DoCheck, OnDestroy {
    private loggedIn: boolean;
    @Input() user: IUser;
    @Input() loginError: boolean;
    @Output() onLogin = new EventEmitter<ICredentials>();
    @Output() onLogout = new EventEmitter();

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

    @Input()
    set isLoggedIn(loggedIn: boolean) {
        this.loggedIn = loggedIn;
    }

    get isLoggedIn(): boolean {
        return this.loggedIn;
    }

    login(credentials: ICredentials, event: Event) {
        event.preventDefault();
        //this._logger.debug(`Login in navmenu-elements: ${credentials.username} & ${credentials.password}`);
        this.onLogin.emit(credentials);
    }

    logout(event: Event) {
        event.preventDefault();
        //this._logger.debug(`Logout in navmenu-elements`);
        this.onLogout.emit();
    }
}