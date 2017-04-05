import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalizationModule } from 'angular-l10n';

import { AuthGuardService } from '../../core/services/authGuard.service';
import { CanDeactivateGuardService } from '../../core/services/canDeactivateGuard.service';

import { SharedModule } from '../../shared/shared.module';

import { HomeComponent } from './home/home.component';

@NgModule({
    declarations: [
        HomeComponent
    ],
    imports: [
        // Angular Modules
        CommonModule,
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
        LocalizationModule.forChild(),
        // My Modules
        SharedModule
    ],
    exports: [HomeComponent],
    providers: [
    ]
})
export class HomeModule {
}