import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalizationModule } from 'angular-l10n';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AuthGuardService } from '../../core/services/auth-guard.service';
import { CanDeactivateGuardService } from '../../core/services/can-deactivate-guard.service';

import { SharedModule } from '../../shared/shared.module';

import { HomeComponent } from './home.component';

@NgModule({
    declarations: [
        HomeComponent
    ],
    imports: [
        // Angular Modules
        RouterModule.forChild([
            {
                path: 'home',
                component: HomeComponent,
                data: { auth: false },
                canActivate: [AuthGuardService],
                canDeactivate: [CanDeactivateGuardService],
                canActivateChild: [AuthGuardService],
                children: []
            }
        ]),
        NgbModule,
        // My Modules
        SharedModule
    ],
    exports: [HomeComponent],
    providers: [
    ]
})
export class HomeModule {
}