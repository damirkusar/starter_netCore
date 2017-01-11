import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { LoggerService } from '../../../../services/logger.service';

@Component({
    selector: 'admin',
    template: require('./admin.component.html'),
    styles: [require('./admin.component.scss')]
})
export class AdminComponent implements OnChanges, OnInit, DoCheck, OnDestroy {
    constructor(private _logger: LoggerService) { }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }

    canDeactivate(): Promise<boolean> | boolean {
        // Allow synchronous navigation (`true`) if no crisis or the crisis is unchanged
        this._logger.debug(`AdminComponent-canDeactivate`);
        // Otherwise ask the user with the dialog service and return its
        // promise which resolves to true or false when the user decides
        return true;
    }
}