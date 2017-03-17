import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule, JsonpModule } from '@angular/http';
import { LocalStorageModule } from 'angular-2-local-storage';
import { FormsModule } from '@angular/forms';
import { LocalizationModule } from 'angular-l10n';

import { AdminModule } from './modules/admin/admin.module';
import { ContactModule } from './modules/contact/contact.module';
import { NavigationModule } from './modules/navigation/navigation.module';
import { NewsRoomModule } from './modules/newsroom/newsroom.module';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';

import { HttpErrorHandlerService } from './services/httpErrorHandler.service';
import { HttpOptionsService } from './services/httpOptions.service';
import { AuthService } from './services/auth.service';
import { AuthGuardService } from './services/authGuard.service';
import { CanDeactivateGuardService } from './services/canDeactivateGuard.service';
import { AccountService } from './services/account.service';
import { LoggerService } from './services/logger.service';
import { LoaderService } from './services/loader.service';

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        HomeComponent
    ],
    imports: [
        // Angular Modules
        BrowserModule,
        BrowserAnimationsModule,
        HttpModule,
        JsonpModule,
        FormsModule,
        RouterModule.forRoot([
            {
                path: 'home',
                component: HomeComponent,
                data: { auth: false },
                canActivate: [AuthGuardService],
                canDeactivate: [CanDeactivateGuardService],
                canActivateChild: [AuthGuardService],
                children: []
            },
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: '**', redirectTo: 'home' }
        ]),
        LocalStorageModule.withConfig({
            prefix: 'webapp',
            storageType: 'localStorage'
        }),
        LocalizationModule.forRoot(),
        // My Modules
        AdminModule,
        ContactModule,
        NavigationModule,
        NewsRoomModule
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
export class AppModule {}