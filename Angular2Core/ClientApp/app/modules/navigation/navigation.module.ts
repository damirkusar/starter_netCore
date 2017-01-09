import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';

import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { NavMenuTopComponent } from './components/navmenu-top/navmenu-top.component';
import { NavMenuElementsLoginComponent } from './components/navmenu-elements-login/navmenu-elements-login.component';
import { NavMenuElementsNotificationComponent } from './components/navmenu-elements-notification/navmenu-elements-notification.component';

@NgModule({
    declarations: [
        NavMenuComponent,
        NavMenuTopComponent,
        NavMenuElementsLoginComponent,
        NavMenuElementsNotificationComponent
    ],
    imports: [
        // Angular Modules
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forRoot([
        ])
        // My Modules
    ],
    exports: [NavMenuComponent],
    providers: [
    ]
})
export class NavigationModule {
}
