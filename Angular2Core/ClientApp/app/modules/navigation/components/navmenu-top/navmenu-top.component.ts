import { Component, OnChanges, OnInit, DoCheck, OnDestroy, EventEmitter, Input, Output } from '@angular/core';
import { ICredentials } from '../../../../models/credentials';
import { IUser } from '../../../../models/user';
import { LoggerService } from '../../../../services/logger.service';

@Component({
    selector: 'navmenu-top',
    template: require('./navmenu-top.component.html'),
    styles: [require('./navmenu-top.component.scss')]

})
export class NavMenuTopComponent implements OnChanges, OnInit, DoCheck, OnDestroy {
    private _isLoggedIn: boolean;
    @Input() user: IUser;
    @Input() loginError: boolean;
    @Output() onLogin = new EventEmitter<ICredentials>();
    @Output() onLogout = new EventEmitter();

    constructor(private _logger: LoggerService) { }

    ngOnChanges(changes: Object): void { }

    ngOnInit(): void { }

    ngDoCheck(): void { }

    ngOnDestroy(): void { }

    @Input()
    set isLoggedIn(isLoggedIn: boolean) {
        this._isLoggedIn = isLoggedIn;
    }

    get isLoggedIn(): boolean {
        return this._isLoggedIn;
    }

    onLoginPerformed(credentials: ICredentials) {
        //this._logger.debug(`OnLoginEvent Received with credentials in navmenu-top: ${credentials.username} & ${credentials.password}`);
        this.onLogin.emit(credentials);
    }

    onLogoutPerformed(event: Event) {
        //this._logger.debug(`OnLogoutEvent Received in navmenu-top`);
        this.onLogout.emit();
    }
}