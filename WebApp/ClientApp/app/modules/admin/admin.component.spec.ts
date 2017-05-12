import { } from 'jasmine';
import { MainSpec } from '../../main.spec';
import { AdminComponent } from './admin.component';

describe('AdminComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(AdminComponent);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof AdminComponent).toBeTruthy();
    });
});