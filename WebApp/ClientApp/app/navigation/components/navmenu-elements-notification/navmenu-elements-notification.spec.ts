import { } from 'jasmine';
import { MainSpec } from '../../../main.spec';
import { NavMenuElementsNotificationComponent } from './navmenu-elements-notification.component';

describe('NavMenuElementsNotificationComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(NavMenuElementsNotificationComponent);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof NavMenuElementsNotificationComponent).toBeTruthy();
    });
});