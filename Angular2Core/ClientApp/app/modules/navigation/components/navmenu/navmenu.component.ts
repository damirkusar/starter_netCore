import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
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
export class NavMenuComponent implements OnChanges, OnInit, DoCheck, OnDestroy {
    isLoggedIn: boolean;
    loginError: boolean;
    loggedInUser: IUser;

    constructor(
        private _logger: LoggerService,
        private _authService: AuthService,
        private _accountService: AccountService,
        private _loaderService: LoaderService) {
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {
            this.isLoggedIn = this._authService.isLoggedIn();
        if (this.isLoggedIn) {
            this._authService.startLogoutTimer();
            this.loggedInUser = this._accountService.getLoggedInUser();
        }

        this._authService.loggedInUpdated.subscribe(
            (isLoggedIn) => {
                this.isLoggedIn = isLoggedIn;
            }
        );
    }

    ngDoCheck(): void { }

    ngOnDestroy(): void {
        this._authService.loggedInUpdated.unsubscribe();
    }

    login(credentials: ICredentials) {
        this._loaderService.setShowModal(true);
        this._authService.login(credentials)
            .subscribe(response => this.loginSucceeded(response),
            error => this.loginFailed(error));
    }

    loginSucceeded(token: IToken) {
        this.loginError = false;
        this.isLoggedIn = this._authService.isLoggedIn();
        this._accountService.getUserInfo()
            .subscribe(response => this.gettingUserInfoSucceeded(response),
            error => this.gettingUserInfoFailed(error));
    }

    loginFailed(error: any) {
        this._logger.error(`NavMenuComponent: Login error: ${JSON.stringify(error)}`);
        this.loginError = true;
        this._loaderService.setShowModal(false);
    }

    gettingUserInfoSucceeded(user: IUser) {
        this.loggedInUser = user;
        this._loaderService.setShowModal(false);
    }

    gettingUserInfoFailed(error: any) {
        this._logger.error(`NavMenuComponent: Getting UserInfo error: ${JSON.stringify(error)}`);
        this._loaderService.setShowModal(false);
    }

    logout(event: Event) {
        this._authService.logout();
        this.isLoggedIn = this._authService.isLoggedIn();
    }
}
