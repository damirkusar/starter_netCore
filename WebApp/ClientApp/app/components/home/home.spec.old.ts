//import { } from 'jasmine';

//import { TestBed, async, ComponentFixture, } from '@angular/core/testing';
//import { APP_BASE_HREF } from '@angular/common';
//import { RouterModule, Router } from '@angular/router';
//import { Localization, LocaleService, TranslationService, LocalizationModule } from 'angular-l10n';
//import { LocalStorageModule, LocalStorageService } from 'angular-2-local-storage';
//import { UniversalModule } from 'angular2-universal';


////import { By } from "@angular/platform-browser";
////import { DebugElement } from "@angular/core";
////import { MockBackend, MockConnection } from '@angular/http/testing';

//import { HomeComponent } from './home.component';
//import { NavigationModule } from '../../modules/navigation/navigation.module';

//import { HttpErrorHandlerService } from '../../services/httpErrorHandler.service';
//import { HttpOptionsService } from '../../services/httpOptions.service';
//import { AuthGuardService } from '../../services/authGuard.service';
//import { CanDeactivateGuardService } from '../../services/canDeactivateGuard.service';

//import { AccountService } from '../../services/account.service';
//import { AuthService } from '../../services/auth.service';
//import { LoggerService } from '../../services/logger.service';
//import { LoaderService } from '../../services/loader.service';

//describe('Home', () => {
//    beforeEach(() => {
//        TestBed.configureTestingModule({
//            declarations: [HomeComponent],
//            imports: [
//                UniversalModule,
//                LocalStorageModule.withConfig({
//                    prefix: 'wepapp-test',
//                    storageType: 'localStorage'
//                }),
//                LocalizationModule.forRoot(),
//                RouterModule.forRoot([]),
//                NavigationModule
//            ],
//            providers: [
//                HttpErrorHandlerService,
//                HttpOptionsService,
//                AuthGuardService,
//                CanDeactivateGuardService,
//                AccountService,
//                AuthService,
//                LoaderService,
//                LoggerService,
//                LocalStorageService,
//                { provide: APP_BASE_HREF, useValue: '/' }
//            ]
//        }).compileComponents();

//        this.fixture = TestBed.createComponent(HomeComponent);
//        this.instance = this.fixture.componentInstance;
//        this.element = this.fixture.nativeElement;
//        this.fixture.detectChanges();
//    });

//    it('should create Component', () => {
//        expect(this.instance instanceof HomeComponent).toBe(true, 'should create HomeComponent');
//    });

//    it(`should have one 'h1' element`, () => {
//        expect(this.element.querySelectorAll('h1').length).toEqual(1);
//    });

//    it(`should have one 'p' element`, () => {
//        expect(this.element.querySelectorAll('p').length).toEqual(1);
//    });

//    it(`should have a h1 element with empty string`, () => {
//        expect(this.element.querySelectorAll('h1')[0].innerText).toEqual(""); // Since no Translations loaded?. 
//    });

//    it(`should have a p element with text 'FullName'`, () => {
//        //this.instance.ngOnInit();
//        //this.fixture.detectChanges();
//        expect(this.element.querySelectorAll('p')[0].innerText).toEqual("WebApp home component");
//    });

//    it(`should have a correct set fullName property`, () => {
//        //this.instance.ngOnInit();
//        expect(this.instance.fullName).toEqual('Guest');
//    });
//});