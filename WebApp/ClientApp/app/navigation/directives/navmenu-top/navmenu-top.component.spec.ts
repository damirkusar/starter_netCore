import { } from 'jasmine';
import { MainSpec } from '../../../main.spec';
import { NavMenuTopComponent } from './navmenu-top.component';

import { NavMenuElementsLocalizationComponent } from '../navmenu-elements-localization/navmenu-elements-localization.component';
import { NavMenuElementsLoginComponent } from '../navmenu-elements-login/navmenu-elements-login.component';
import { NavMenuElementsNotificationComponent } from '../navmenu-elements-notification/navmenu-elements-notification.component';

describe('NavMenuTopComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(NavMenuTopComponent,
            [
                NavMenuElementsLocalizationComponent,
                NavMenuElementsLoginComponent,
                NavMenuElementsNotificationComponent
            ]);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof NavMenuTopComponent).toBeTruthy();
    });
});