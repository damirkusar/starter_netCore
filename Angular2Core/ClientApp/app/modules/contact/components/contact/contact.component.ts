import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { LoggerService } from '../../../../services/logger.service';

@Component({
    selector: 'contact',
    template: require('./contact.component.html'),
    styles: [require('./contact.component.scss')]
})
export class ContactComponent implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(private _logger: LoggerService) { }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }
}
