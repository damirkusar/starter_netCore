import { Injectable, EventEmitter } from '@angular/core';
import { CanActivate, CanActivateChild, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { LoggerService } from './logger.service';
import { LocalStorageService } from 'angular-2-local-storage';

@Injectable()
export class AuthGuardService implements CanActivate, CanActivateChild {

    constructor(private _logger: LoggerService, private _localStorage: LocalStorageService, private _authService: AuthService, private _router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let canActivate = true;
        
        if (route.data['auth']) {
            this._logger.log(`Auth: ${route.data['auth']}`);
            canActivate = this._authService.isLoggedIn();
        }

        if (canActivate && route.data['roles']) {
            this._logger.log(`Allowed roles: ${route.data['roles']}`);
            canActivate = this._authService.isInRole(route.data['roles']);
        }

        if (!canActivate) {
            this._logger.error(`Can not activate: ${state.url}`);
            this._router.navigate(['/home']); 
        }

        return canActivate;
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivate(route, state);
    }
}