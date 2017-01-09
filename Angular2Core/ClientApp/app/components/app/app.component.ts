import { Component, OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { LoaderService } from '../../services/loader.service';
import { LoggerService } from '../../services/logger.service';

@Component({
    selector: 'app',
    template: require('./app.component.html'),
    styles: [require('./app.component.scss')]
})
export class AppComponent implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy {
    showModal: boolean;

    constructor(private _logger: LoggerService, private _loaderService: LoaderService) { }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void {
        this._loaderService.showModalUpdated.subscribe(
            (show) => {
                this.showModal = show;
            }
        );
    }

    ngDoCheck(): void { }

    ngAfterContentInit(): void { }

    ngAfterContentChecked(): void { }

    ngAfterViewInit(): void { }

    ngAfterViewChecked(): void { }

    ngOnDestroy(): void {
        this._loaderService.showModalUpdated.unsubscribe();
    }
}
