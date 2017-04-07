import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalizationModule } from 'angular-l10n';

import { AuthGuardService } from '../core/services/authGuard.service';
import { CanDeactivateGuardService } from '../core/services/canDeactivateGuard.service';

import { SharedModule } from '../shared/shared.module';

import { NavigationComponent } from './components/navigation/navigation.component';

import { NavMenuTopComponent } from './directives/navmenu-top/navmenu-top.component';
import { NavMenuElementsLocalizationComponent } from './directives/navmenu-elements-localization/navmenu-elements-localization.component';
import { NavMenuElementsLoginComponent } from './directives/navmenu-elements-login/navmenu-elements-login.component';
import { NavMenuElementsNotificationComponent } from './directives/navmenu-elements-notification/navmenu-elements-notification.component';

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
        RouterModule.forRoot([
        ]),
        // My Modules
        SharedModule
    ],
    exports: [NavigationComponent],
    providers: [
    ]
})
export class NavigationModule {
}
