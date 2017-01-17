import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';

import { LocaleModule, LocalizationModule, LocaleService, LocalizationService } from 'angular2localization';

import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { NavMenuTopComponent } from './components/navmenu-top/navmenu-top.component';
import { NavMenuElementsLocalizationComponent } from './components/navmenu-elements-localization/navmenu-elements-localization.component';
import { NavMenuElementsLoginComponent } from './components/navmenu-elements-login/navmenu-elements-login.component';
import { NavMenuElementsNotificationComponent } from './components/navmenu-elements-notification/navmenu-elements-notification.component';

@NgModule({
    declarations: [
        NavMenuComponent,
        NavMenuTopComponent,
        NavMenuElementsLocalizationComponent,
        NavMenuElementsLoginComponent,
        NavMenuElementsNotificationComponent
    ],
    imports: [
        // Angular Modules
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forRoot([
        ]),
        LocaleModule.forChild(),
        LocalizationModule.forChild()
        // My Modules
    ],
    exports: [NavMenuComponent],
    providers: [
    ]
})
export class NavigationModule {
}
