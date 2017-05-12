import { } from 'jasmine';
import { MainSpec } from '../../main.spec';
import { HomeComponent } from './home.component';

describe('HomeComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(HomeComponent);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof HomeComponent).toBeTruthy();
    });

    it(`should have one 'h1' element`, () => {
        expect(this.spec.element.querySelectorAll('h1').length).toEqual(1);
    });

    it(`should have one 'p' element`, () => {
        expect(this.spec.element.querySelectorAll('p').length).toEqual(1);
    });

    it(`should have a h1 element with empty string`, () => {
        expect(this.spec.element.querySelectorAll('h1')[0].innerText).toEqual(""); // Since no Translations loaded?. 
    });

    it(`should have a p element with text 'FullName'`, () => {
        //this.instance.ngOnInit();
        //this.fixture.detectChanges();
        expect(this.spec.element.querySelectorAll('p')[0].innerText).toEqual("WebApp home component");
    });

    it(`should have a correct set fullName property`, () => {
        //this.instance.ngOnInit();
        expect(this.spec.instance.fullName).toEqual('Guest');
    });
});