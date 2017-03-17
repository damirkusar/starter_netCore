import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule, JsonpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { LocalizationModule } from 'angular-l10n';

import { AdminComponent } from './admin/admin.component';

import { AuthGuardService } from '../../services/authGuard.service';
import { CanDeactivateGuardService } from '../../services/canDeactivateGuard.service';

@NgModule({
    declarations: [
        AdminComponent
    ],
    imports: [
        // Angular Modules
        //UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        BrowserModule,
        BrowserAnimationsModule,
        HttpModule,
        JsonpModule,
        FormsModule,
        RouterModule.forChild([
            {
                path: 'admin',
                component: AdminComponent,
                data: { auth: true, roles: ['admin'] },
                canActivate: [AuthGuardService],
                canDeactivate: [CanDeactivateGuardService],
                canActivateChild: [AuthGuardService],
                children: []
            }
        ]),
        LocalizationModule.forChild()
        // My Modules
    ],
    exports: [AdminComponent],
    providers: [
    ]
})
export class AdminModule {
}
