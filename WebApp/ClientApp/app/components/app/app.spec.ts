import { } from 'jasmine';
import { MainSpec } from '../../main.spec';
import { AppComponent } from './app.component';

import { NavigationModule } from '../../modules/navigation/navigation.module';


describe('AppComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(AppComponent,
            [],
            [
                NavigationModule
            ]
        );
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof AppComponent).toBeTruthy();
    });
});