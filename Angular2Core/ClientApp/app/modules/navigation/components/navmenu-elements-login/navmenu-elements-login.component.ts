import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Locale, LocaleService, LocalizationService } from 'angular2localization';
import { IUser } from '../../../../models/user';
import { ICredentials } from '../../../../models/credentials';
import { LoggerService } from '../../../../services/logger.service';

@Component({
    selector: 'navmenu-elements-login',
    template: require('./navmenu-elements-login.component.html'),
    styles: [require('./navmenu-elements-login.component.scss')]

})
export class NavMenuElementsLoginComponent extends Locale implements OnChanges, OnInit, DoCheck, OnDestroy {
    private _isLoggedIn: boolean;
    @Input() user: IUser;
    @Input() loginError: boolean;
    @Output() onLogin = new EventEmitter<ICredentials>();
    @Output() onLogout = new EventEmitter();

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

    @Input()
    set isLoggedIn(loggedIn: boolean) {
        this._isLoggedIn = loggedIn;
    }

    get isLoggedIn(): boolean {
        return this._isLoggedIn;
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