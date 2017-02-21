import { Injectable, EventEmitter } from '@angular/core';
import { CanActivate, CanActivateChild, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { LoggerService } from './logger.service';
import { LocalStorageService } from 'angular-2-local-storage';

@Injectable()
export class AuthGuardService implements CanActivate, CanActivateChild {

    constructor(private logger: LoggerService, private localStorage: LocalStorageService, private authService: AuthService, private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let canActivate = true;
        
        if (route.data['auth']) {
            this.logger.log(`Auth: ${route.data['auth']}`);
            canActivate = this.authService.isLoggedIn();
        }

        if (canActivate && route.data['roles']) {
            this.logger.log(`Allowed roles: ${route.data['roles']}`);
            canActivate = this.authService.isInRole(route.data['roles']);
        }

        if (!canActivate) {
            this.logger.error(`Can not activate: ${state.url}`);
            this.router.navigate(['/home']); 
        }

        return canActivate;
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivate(route, state);
    }
}