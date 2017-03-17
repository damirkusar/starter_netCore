import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule, JsonpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { LocalizationModule } from 'angular-l10n';

import { NavigationComponent } from './navigation/navigation.component';

import { NavMenuTopComponent } from './components/navmenu-top/navmenu-top.component';
import { NavMenuElementsLocalizationComponent } from './components/navmenu-elements-localization/navmenu-elements-localization.component';
import { NavMenuElementsLoginComponent } from './components/navmenu-elements-login/navmenu-elements-login.component';
import { NavMenuElementsNotificationComponent } from './components/navmenu-elements-notification/navmenu-elements-notification.component';

@NgModule({
    declarations: [
        NavigationComponent,
        NavMenuTopComponent,
        NavMenuElementsLocalizationComponent,
        NavMenuElementsLoginComponent,
        NavMenuElementsNotificationComponent
    ],
    imports: [
        // Angular Modules
        BrowserModule,
        BrowserAnimationsModule,
        HttpModule,
        JsonpModule,
        FormsModule,
        RouterModule.forRoot([
        ]),
        LocalizationModule.forChild()
        // My Modules
    ],
    exports: [NavigationComponent],
    providers: [
    ]
})
export class NavigationModule {
}
