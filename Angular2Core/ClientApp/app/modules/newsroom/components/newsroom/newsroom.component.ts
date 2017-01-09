import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { LoggerService } from '../../../../services/logger.service';

@Component({
    selector: 'news-room',
    template: require('./newsroom.component.html'),
    styles: [require('./newsroom.component.scss')]
})
export class NewsRoomComponent implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(private _logger: LoggerService) { }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }
}
