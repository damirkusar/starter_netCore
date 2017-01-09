import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { LoggerService } from '../../../../services/logger.service';

@Component({
    selector: 'navmenu-elements-notification',
    template: require('./navmenu-elements-notification.component.html'),
    styles: [require('./navmenu-elements-notification.component.scss')]

})
export class NavMenuElementsNotificationComponent implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(private _logger: LoggerService) { }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }
}