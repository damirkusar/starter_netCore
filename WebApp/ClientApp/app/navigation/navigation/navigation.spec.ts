import { } from 'jasmine';
import { MainSpec } from '../../main.spec';
import { NavigationComponent } from './navigation.component';

import { NavMenuTopComponent } from '../components/navmenu-top/navmenu-top.component';
import { NavMenuElementsLocalizationComponent } from '../components/navmenu-elements-localization/navmenu-elements-localization.component';
import { NavMenuElementsLoginComponent } from '../components/navmenu-elements-login/navmenu-elements-login.component';
import { NavMenuElementsNotificationComponent } from '../components/navmenu-elements-notification/navmenu-elements-notification.component';

describe('NavigationComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(NavigationComponent,
            [
                NavMenuTopComponent,
                NavMenuElementsLocalizationComponent,
                NavMenuElementsLoginComponent,
                NavMenuElementsNotificationComponent
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