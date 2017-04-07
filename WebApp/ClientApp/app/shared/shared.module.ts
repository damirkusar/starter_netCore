import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule, JsonpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { LocalizationModule } from 'angular-l10n';

import { SiteHeaderComponent } from './directives/siteheader/siteheader.component';

import { FilterPipe } from './pipes/filter.pipe';
import { ReplacePipe } from './pipes/replace.pipe';

@NgModule({
    declarations: [
        // Pipes
        FilterPipe,
        ReplacePipe,
        // Components
        SiteHeaderComponent
    ],
    imports: [
        // Angular Modules
        CommonModule,
        BrowserAnimationsModule,
        HttpModule,
        JsonpModule,
        FormsModule,
        RouterModule.forChild([]),
        LocalizationModule.forChild()
        // My Modules
    ],
    exports: [
        LocalizationModule,
        // Main
        CommonModule,
        BrowserAnimationsModule,
        HttpModule,
        JsonpModule,
        FormsModule,
        // Pipes
        FilterPipe,
        ReplacePipe,
        // Components
        SiteHeaderComponent
    ],
    providers: []
})
export class SharedModule {
}
