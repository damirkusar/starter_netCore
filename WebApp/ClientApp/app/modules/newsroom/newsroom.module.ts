import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalizationModule } from 'angular-l10n';

import { AuthGuardService } from '../../core/services/auth-guard.service';
import { CanDeactivateGuardService } from '../../core/services/can-deactivate-guard.service';

import { SharedModule } from '../../shared/shared.module';

import { NewsRoomComponent } from './newsroom.component';
import { NewsService } from './services/news.service';
import { NewsResolverService } from './services/newsResolver.service';

@NgModule({
    declarations: [
        NewsRoomComponent
    ],
    imports: [
        // Angular Modules
        RouterModule.forChild([
            {
                path: 'news-room',
                component: NewsRoomComponent,
                data: { auth: true, roles:['admin'] },
                resolve: { news: NewsResolverService },
                canActivate: [AuthGuardService],
                canDeactivate: [CanDeactivateGuardService],
                canActivateChild: [AuthGuardService],
                children: []
            }
        ]),
        // My Modules
        SharedModule
    ],
    exports: [NewsRoomComponent],
    providers: [
        NewsService,
        NewsResolverService
    ]
})
export class NewsRoomModule {
}