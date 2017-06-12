import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule, JsonpModule } from '@angular/http';
import { LocalStorageModule, ILocalStorageServiceConfig } from 'angular-2-local-storage';
import { FormsModule } from '@angular/forms';
import { LocalizationModule } from 'angular-l10n';

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { NavigationModule } from './navigation/navigation.module';

import { AppComponent } from './app.component';

import { AuthGuardService } from './core/services/auth-guard.service';
import { CanDeactivateGuardService } from './core/services/can-deactivate-guard.service';

import { HomeModule } from './modules/home/home.module';
import { NewsRoomModule } from './modules/newsroom/newsroom.module';

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent
    ],
    imports: [
        // Angular Modules
        BrowserModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: '**', redirectTo: 'home' }
        ]),
        LocalStorageModule.withConfig({
            prefix: 'angularXcore',
            storageType: 'localStorage'
        }),
        LocalizationModule.forRoot(),
        // My Modules
        CoreModule,
        SharedModule,
        HomeModule,
        NavigationModule,
        NewsRoomModule
    ],
    providers: [
    ]
})
export class AppModule {}