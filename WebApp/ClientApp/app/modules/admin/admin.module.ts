import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalizationModule } from 'angular-l10n';

import { AuthGuardService } from '../shared/services/authGuard.service';
import { CanDeactivateGuardService } from '../shared/services/canDeactivateGuard.service';

import { SharedModule } from '../shared/shared.module';

import { AdminComponent } from './admin/admin.component';

@NgModule({
    declarations: [
        AdminComponent
    ],
    imports: [
        // Angular Modules
        CommonModule,
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
        LocalizationModule.forChild(),
        // My Modules
        SharedModule
    ],
    exports: [AdminComponent],
    providers: [
    ]
})
export class AdminModule {
}