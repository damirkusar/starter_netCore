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
    greeting: string;

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

        this.accountService.loggedInUserUpdated.subscribe(
            (user) => {
                this.setFullName();
            }
        );

        this.translation.translationChanged.subscribe(
            () => {
                this.translate();
            }
        );
    }

    setFullName(): void {
        let currentUser: IUser = this.accountService.getLoggedInUser();
        if (currentUser != null) {
            this.fullName = `${currentUser.fullName}`;
        } else {
            this.fullName = 'Guest';
        }
        this.translate();
    }

    translate() {
        this.greeting = this.translation.translate('welcome', { fullname: this.fullName });
    }

    ngDoCheck(): void { }

    ngAfterContentInit(): void { }

    ngAfterContentChecked(): void { }

    ngAfterViewInit(): void { }

    ngAfterViewChecked(): void { }

    ngOnDestroy(): void { }
}
