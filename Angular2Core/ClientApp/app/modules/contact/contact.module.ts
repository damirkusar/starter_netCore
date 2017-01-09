import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';

import { ContactComponent } from './components/contact/contact.component';

@NgModule({
    declarations: [
        ContactComponent
    ],
    imports: [
        // Angular Modules
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forRoot([
            { path: 'contact', component: ContactComponent }
        ])
        // My Modules
    ],
    exports: [ContactComponent],
    providers: [
    ]
})
export class ContactModule {
}
