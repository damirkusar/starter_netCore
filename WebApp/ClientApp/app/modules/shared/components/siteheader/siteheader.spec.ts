import { } from 'jasmine';
import { MainSpec } from '../../../../main.spec';
import { SiteHeaderComponent } from './siteheader.component';

describe('SiteHeaderComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.initForSharedComponents(SiteHeaderComponent, null, null, null);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof SiteHeaderComponent).toBeTruthy();
    });
});