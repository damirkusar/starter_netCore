import { Injectable, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import './rxjs-operators';
import { LoggerService } from './logger.service';

@Injectable()
export class LoaderService implements OnChanges, OnInit, DoCheck, OnDestroy {
    showModal: boolean = false;
    showModalUpdated = new EventEmitter();

    constructor(private _logger: LoggerService, private _http: Http) {}

    ngOnChanges(changes: Object): void {}

    ngOnInit(): void {}

    ngDoCheck(): void {}

    ngOnDestroy(): void {}

    setShowModal(show: boolean) {
        this.showModal = show;
        this.showModalUpdated.emit(this.showModal);
    }

    getShowModal() {
        return this.showModal;
    }
}