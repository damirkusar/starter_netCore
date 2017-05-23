import { } from 'jasmine';
import { MainSpec } from '../main.spec';
import { NavigationComponent } from './navigation.component';

import { NavMenuTopComponent } from './directives/navmenu-top/navmenu-top.component';
import { NavMenuElementsLocalizationComponent } from './directives/navmenu-elements-localization/navmenu-elements-localization.component';
import { NavMenuElementsLoginComponent } from './directives/navmenu-elements-login/navmenu-elements-login.component';

describe('NavigationComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(NavigationComponent,
            [
                NavMenuTopComponent,
                NavMenuElementsLocalizationComponent,
                NavMenuElementsLoginComponent
            ]);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof NavigationComponent).toBeTruthy();
    });

    it(`isLoggedIn property is falsy`, () => {
        expect(this.spec.instance.isLoggedIn).toBeFalsy();
    });
});