import { } from 'jasmine';
import { MainSpec } from '../../../main.spec';
import { NewsRoomComponent } from './newsroom.component';

describe('NewsRoomComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(NewsRoomComponent, null, null, null);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof NewsRoomComponent).toBeTruthy();
    });
});