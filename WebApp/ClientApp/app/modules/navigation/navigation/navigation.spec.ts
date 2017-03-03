import { } from 'jasmine';
import { MainSpec } from '../../../main.spec';
import { NavigationComponent } from './navigation.component';

describe('NavigationComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(NavigationComponent);
    });

    //it('should create Component', () => {
    //    expect(this.spec.instance instanceof NavigationComponent).toBe(true, 'should create Component');
    //});
});