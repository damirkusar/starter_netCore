import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule, JsonpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { LocalizationModule } from 'angular-l10n';

import { AccountService } from './services/account.service';
import { AuthGuardService } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { CanDeactivateGuardService } from './services/can-deactivate-guard.service';
import { HttpErrorHandlerService } from './services/http-error-handler.service';
import { HttpOptionsService } from './services/http-options.service';
import { LoaderService } from './services/loader.service';
import { LoggerService } from './services/logger.service';


@NgModule({
    declarations: [
    ],
    imports: [
        // Angular Modules
        CommonModule,
        HttpModule,
        JsonpModule,
        FormsModule,
        RouterModule.forChild([]),
        LocalizationModule.forChild()
        // My Modules
    ],
    exports: [
        // Main
    ],
    providers: [
        HttpErrorHandlerService,
        HttpOptionsService,
        AuthGuardService,
        CanDeactivateGuardService,
        AuthService,
        AccountService,
        LoggerService,
        LoaderService
    ]
})
export class CoreModule {
}
