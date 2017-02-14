import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';

import { LocalizationModule } from 'angular-l10n';

import { ContactComponent } from './contact/contact.component';

import { AuthGuardService } from '../../services/authGuard.service';
import { CanDeactivateGuardService } from '../../services/canDeactivateGuard.service';

@NgModule({
    declarations: [
        ContactComponent
    ],
    imports: [
        // Angular Modules
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forChild([
            {
                path: 'contact',
                component: ContactComponent,
                data: { auth: true },
                canActivate: [AuthGuardService],
                canDeactivate: [CanDeactivateGuardService],
                canActivateChild: [AuthGuardService],
                children: []
            }
        ]),
        LocalizationModule.forChild()
        // My Modules
    ],
    exports: [ContactComponent],
    providers: [
    ]
})
export class ContactModule {
}
