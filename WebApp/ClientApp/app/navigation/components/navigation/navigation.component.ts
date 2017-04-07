import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';

import { ICredentials } from '../../../shared/models/credentials';
import { IUser } from '../../../shared/models/user';
import { IToken } from '../../../shared/models/token';
import { AccountService } from '../../../core/services/account.service';
import { AuthService } from '../../../core/services/auth.service';
import { LoaderService } from '../../../core/services/loader.service';
import { LoggerService } from '../../../core/services/logger.service';

@Component({
    selector: 'navigation',
    template: require('./navigation.component.html'),
    styles: [require('./navigation.component.scss')]
})
export class NavigationComponent extends Localization implements OnChanges, OnInit, DoCheck, OnDestroy {
    isLoggedIn: boolean;
    loginError: boolean;
    loggedInUser: IUser;

    constructor(
        private logger: LoggerService,
        private authService: AuthService,
        private accountService: AccountService,
        private loaderService: LoaderService,
        public locale: LocaleService,
        public translation: TranslationService) {
        super(locale, translation);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {
            this.isLoggedIn = this.authService.isLoggedIn();
        if (this.isLoggedIn) {
            this.authService.startLogoutTimer();
            this.loggedInUser = this.accountService.getLoggedInUser();
        }

        this.authService.loggedInUpdated.subscribe(
            (isLoggedIn) => {
                this.isLoggedIn = isLoggedIn;
            }
        );
    }

    ngDoCheck(): void { }

    ngOnDestroy(): void {
        this.authService.loggedInUpdated.unsubscribe();
    }

    login(credentials: ICredentials) {
        this.loaderService.setShowModal(true);
        this.authService.login(credentials)
            .subscribe(response => this.loginSucceeded(response),
            error => this.loginFailed(error));
    }

    loginSucceeded(token: IToken) {
        this.loginError = false;
        this.isLoggedIn = this.authService.isLoggedIn();
        this.accountService.getUserInfo()
            .subscribe(response => this.gettingUserInfoSucceeded(response),
            error => this.gettingUserInfoFailed(error));
    }

    loginFailed(error: any) {
        this.logger.error(`NavMenuComponent: Login error: ${JSON.stringify(error)}`);
        this.loginError = true;
        this.loaderService.setShowModal(false);
    }

    gettingUserInfoSucceeded(user: IUser) {
        this.loggedInUser = user;
        this.loaderService.setShowModal(false);
    }

    gettingUserInfoFailed(error: any) {
        this.logger.error(`NavMenuComponent: Getting UserInfo error: ${JSON.stringify(error)}`);
        this.loaderService.setShowModal(false);
    }

    logout(event: Event) {
        this.authService.logout();
        this.isLoggedIn = this.authService.isLoggedIn();
    }
}
