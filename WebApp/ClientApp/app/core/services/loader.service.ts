import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { LoggerService } from './logger.service';

@Injectable()
export class LoaderService {
    showModal: boolean = false;
    showModalUpdated = new EventEmitter();

    constructor(private logger: LoggerService, private http: Http) {}

    setShowModal(show: boolean) {
        this.showModal = show;
        this.showModalUpdated.emit(this.showModal);
    }

    getShowModal() {
        return this.showModal;
    }
}