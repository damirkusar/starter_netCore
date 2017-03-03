import { } from 'jasmine';
import { MainSpec } from '../../main.spec';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(AppComponent);
    });

    //it('should create Component', () => {
    //    expect(this.spec.instance instanceof AppComponent).toBeTruthy();
    //});
});