import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';

import { NewsRoomComponent } from './components/newsroom/newsroom.component';

@NgModule({
    declarations: [
        NewsRoomComponent
    ],
    imports: [
        // Angular Modules
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forRoot([
            { path: 'news-room', component: NewsRoomComponent }
        ])
        // My Modules
    ],
    exports: [NewsRoomComponent],
    providers: [
    ]
})
export class NewsRoomModule {
}
