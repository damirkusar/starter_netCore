import { Component, OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { LoggerService } from '../../services/logger.service';

@Component({
    selector: 'home',
    template: require('./home.component.html'),
    styles: [require('./home.component.scss')]
})
export class HomeComponent implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy {
    constructor(private _logger: LoggerService) { }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngAfterContentInit(): void { }

    ngAfterContentChecked(): void { }

    ngAfterViewInit(): void { }

    ngAfterViewChecked(): void { }

    ngOnDestroy(): void { }
}
