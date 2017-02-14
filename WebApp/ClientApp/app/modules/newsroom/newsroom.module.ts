import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';

import { LocalizationModule } from 'angular-l10n';

import { NewsRoomComponent } from './newsroom/newsroom.component';

import { AuthGuardService } from '../../services/authGuard.service';
import { CanDeactivateGuardService } from '../../services/canDeactivateGuard.service';

import { NewsService } from './services/news.service';
import { NewsResolverService } from './services/newsResolver.service';

@NgModule({
    declarations: [
        NewsRoomComponent
    ],
    imports: [
        // Angular Modules
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forChild([
            {
                path: 'news-room',
                component: NewsRoomComponent,
                data: { auth: true },
                resolve: { news: NewsResolverService },
                canActivate: [AuthGuardService],
                canDeactivate: [CanDeactivateGuardService],
                canActivateChild: [AuthGuardService],
                children: []
            }
        ]),
        LocalizationModule.forChild()
        // My Modules
    ],
    exports: [NewsRoomComponent],
    providers: [
        NewsService,
        NewsResolverService
    ]
})
export class NewsRoomModule {
}
