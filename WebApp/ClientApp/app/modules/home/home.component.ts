import { Component, OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { Localization, LocaleService, TranslationService } from 'angular-l10n';
import { IUser } from '../../shared/models/models';
import { AccountService } from '../../core/services/account.service';
import { AuthService } from '../../core/services/auth.service';
import { LoggerService } from '../../core/services/logger.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent extends Localization implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy {
    fullName: string;

    constructor(private logger: LoggerService, private authService: AuthService, private accountService: AccountService, public translation: TranslationService, public locale: LocaleService) {
        super(locale, translation);
    }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {
        this.setFullName();
        this.authService.loggedInUpdated.subscribe(
            (isLoggedIn) => {
                if (!isLoggedIn) {
                    this.fullName = 'Guest';
                }
            }
        );

        this.authService.loggedInUserUpdated.subscribe(
            (user) => {
                if (user != null) {
                    //this.fullName = `${user.firstName} ${user.lastName}`;
                    this.setFullName();
                }
            }
        );
    }

    setFullName(): void {
        let currentUser: IUser = this.authService.getLoggedInUser();
        this.logger.log("Hello current", currentUser.firstName);
        if (currentUser != null) {
            this.fullName = `${currentUser.firstName} ${currentUser.lastName}`;
        } else {
            this.fullName = 'Guest';
        }
    }

    ngDoCheck(): void { }

    ngAfterContentInit(): void { }

    ngAfterContentChecked(): void { }

    ngAfterViewInit(): void { }

    ngAfterViewChecked(): void { }

    ngOnDestroy(): void { }
}
