import { } from 'jasmine';
import { MainSpec } from '../../../../main.spec';
import { NavMenuElementsLocalizationComponent } from './navmenu-elements-localization.component';

describe('NavMenuElementsLocalizationComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(NavMenuElementsLocalizationComponent);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof NavMenuElementsLocalizationComponent).toBeTruthy();
    });
});