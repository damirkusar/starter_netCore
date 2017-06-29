import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';

import { IUser, ICredentials } from '../shared/models/models';
import { AccountService } from '../core/services/account.service';
import { AuthService } from '../core/services/auth.service';
import { LoaderService } from '../core/services/loader.service';
import { LoggerService } from '../core/services/logger.service';

@Component({
    selector: 'navigation',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.scss']
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
        this.loggedInUser = this.accountService.getLoggedInUser();
        this.accountService.loggedInUserUpdated.subscribe((user: IUser) => {
            this.loggedInUser = user;
        });

        this.authService.loggedInUpdated.subscribe(
            (isLoggedIn) => {
                this.isLoggedIn = isLoggedIn;
            }
        );
    }

    ngDoCheck(): void { }

    ngOnDestroy(): void {
        this.accountService.loggedInUserUpdated.unsubscribe();
        this.authService.loggedInUpdated.unsubscribe();
    }

    login(credentials: ICredentials) {
        this.authService.login(credentials);
    }

    logout(event: Event) {
        this.authService.logout();
    }
}
