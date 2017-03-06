import { } from 'jasmine';
import { MainSpec } from '../../../../main.spec';
import { NavMenuElementsLoginComponent } from './navmenu-elements-login.component';

describe('NavMenuElementsLoginComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(NavMenuElementsLoginComponent);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof NavMenuElementsLoginComponent).toBeTruthy();
    });
});