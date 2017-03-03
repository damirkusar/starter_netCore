import { } from 'jasmine';

import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { APP_BASE_HREF } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { Localization, LocaleService, TranslationService, LocalizationModule } from 'angular-l10n';
import { LocalStorageModule, LocalStorageService } from 'angular-2-local-storage';
import { UniversalModule } from 'angular2-universal';

//import { By } from "@angular/platform-browser";
//import { DebugElement } from "@angular/core";
//import { MockBackend, MockConnection } from '@angular/http/testing';

//import { NavigationModule } from './modules/navigation/navigation.module';

import { HttpErrorHandlerService } from './services/httpErrorHandler.service';
import { HttpOptionsService } from './services/httpOptions.service';
import { AuthGuardService } from './services/authGuard.service';
import { CanDeactivateGuardService } from './services/canDeactivateGuard.service';

import { AccountService } from './services/account.service';
import { AuthService } from './services/auth.service';
import { LoggerService } from './services/logger.service';
import { LoaderService } from './services/loader.service';

export class MainSpec {
        fixture: any;
        instance: any;
        element: any;
        constructor() { }

        init(component): void {
            TestBed.configureTestingModule({
                declarations: [component],
                imports: [
                    UniversalModule,
                    LocalStorageModule.withConfig({
                        prefix: 'wepapp-test',
                        storageType: 'localStorage'
                    }),
                    LocalizationModule.forRoot(),
                    RouterModule.forRoot([]),
                    //NavigationModule
                ],
                providers: [
                    HttpErrorHandlerService,
                    HttpOptionsService,
                    AuthGuardService,
                    CanDeactivateGuardService,
                    AccountService,
                    AuthService,
                    LoaderService,
                    LoggerService,
                    LocalStorageService,
                    { provide: APP_BASE_HREF, useValue: '/' }
                ]
        }).compileComponents();

        this.fixture = TestBed.createComponent(component);
        this.instance = this.fixture.componentInstance;
        this.element = this.fixture.nativeElement;
        this.fixture.detectChanges();
    }
}