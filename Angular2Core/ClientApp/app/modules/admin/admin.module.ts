import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';

import { AdminComponent } from './components/admin/admin.component';

@NgModule({
    declarations: [
        AdminComponent
    ],
    imports: [
        // Angular Modules
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forRoot([
            { path: 'admin', component: AdminComponent }
        ])
        // My Modules
    ],
    exports: [AdminComponent],
    providers: [
    ]
})
export class AdminModule {
}
