import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Locale, LocaleService, LocalizationService } from 'angular2localization';
import { ICredentials } from '../../../../models/credentials';
import { IUser } from '../../../../models/user';
import { IToken } from '../../../../models/token';
import { AccountService } from '../../../../services/account.service';
import { AuthService } from '../../../../services/auth.service';
import { LoaderService } from '../../../../services/loader.service';
import { LoggerService } from '../../../../services/logger.service';

@Component({
    selector: 'nav-menu',
    template: require('./navmenu.component.html'),
    styles: [require('./navmenu.component.scss')]
})
export class NavMenuComponent extends Locale implements OnChanges, OnInit, DoCheck, OnDestroy {
    isLoggedIn: boolean;
    loginError: boolean;
    loggedInUser: IUser;

    constructor(
        private logger: LoggerService,
        private authService: AuthService,
        private accountService: AccountService,
        private loaderService: LoaderService,
        public locale: LocaleService,
        public localization: LocalizationService) {
        super(locale, localization);
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
