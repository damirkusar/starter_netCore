import { } from 'jasmine';
import { MainSpec } from '../../../main.spec';
import { AdminComponent } from './admin.component';

describe('AdminComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(AdminComponent);
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof AdminComponent).toBe(true, 'should create Component');
    });
});