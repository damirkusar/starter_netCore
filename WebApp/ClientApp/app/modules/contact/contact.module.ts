import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule, JsonpModule } from '@angular/http';
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
        BrowserModule,
        BrowserAnimationsModule,
        HttpModule,
        JsonpModule,
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
