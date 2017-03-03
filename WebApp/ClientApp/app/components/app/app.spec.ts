import { } from 'jasmine';
import { MainSpec } from '../../main.spec';
import { AppComponent } from './app.component';

import { NavigationComponent } from '../../modules/navigation/navigation/navigation.component';
import { NavMenuTopComponent } from '../../modules/navigation/components/navmenu-top/navmenu-top.component';
import { NavMenuElementsLocalizationComponent } from '../../modules/navigation/components/navmenu-elements-localization/navmenu-elements-localization.component';
import { NavMenuElementsLoginComponent } from '../../modules/navigation/components/navmenu-elements-login/navmenu-elements-login.component';
import { NavMenuElementsNotificationComponent } from '../../modules/navigation/components/navmenu-elements-notification/navmenu-elements-notification.component';

describe('AppComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(AppComponent,
            [
                NavigationComponent,
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
        expect(this.spec.instance instanceof AppComponent).toBeTruthy();
    });
});