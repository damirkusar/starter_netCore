import { } from 'jasmine';
import { MainSpec } from '../../main.spec';
import { ContactComponent } from './contact.component';

describe('ContactComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(ContactComponent);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof ContactComponent).toBeTruthy();
    });
});