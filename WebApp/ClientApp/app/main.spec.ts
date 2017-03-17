import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule, JsonpModule } from '@angular/http';
import { APP_BASE_HREF } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router, Resolve, RouterStateSnapshot, ActivatedRoute } from '@angular/router';
import { Localization, LocaleService, TranslationService, LocalizationModule } from 'angular-l10n';
import { LocalStorageModule, LocalStorageService } from 'angular-2-local-storage';

//import { By } from "@angular/platform-browser";
//import { DebugElement } from "@angular/core";
//import { MockBackend, MockConnection } from '@angular/http/testing';

import { HttpErrorHandlerService } from './services/httpErrorHandler.service';
import { HttpOptionsService } from './services/httpOptions.service';
import { AuthGuardService } from './services/authGuard.service';
import { CanDeactivateGuardService } from './services/canDeactivateGuard.service';

import { AccountService } from './services/account.service';
import { AuthService } from './services/auth.service';
import { LoggerService } from './services/logger.service';
import { LoaderService } from './services/loader.service';

export class MainSpec {
    fixture: any;
    instance: any;
    element: any;
    testBed: any;
    constructor() { }

    init(component, additionalDeclarations, additionalImports, additionalProviders): void {
        console.info('Initalizing Tests for', component.name);
        let declarations = [component];

        let imports = [
            BrowserAnimationsModule,
            BrowserModule,
            HttpModule,
            JsonpModule,
            FormsModule,
            LocalStorageModule.withConfig({
                prefix: 'wepapp-test',
                storageType: 'localStorage'
            }),
            LocalizationModule.forRoot(),
            RouterModule.forRoot([])
        ];

        let providers = [
            { provide: APP_BASE_HREF, useValue: '/' },
            HttpErrorHandlerService,
            HttpOptionsService,
            AuthGuardService,
            CanDeactivateGuardService,
            AccountService,
            AuthService,
            LoaderService,
            LoggerService
        ];

        //console.info('additionalDeclarations', additionalDeclarations);
        //console.info('additionalImports', additionalImports);
        //console.info('additionalProviders', additionalProviders);

        if (additionalDeclarations) {
            for (let ad of additionalDeclarations) {
                declarations.push(ad);
            }
        }

        if (additionalImports) {
            for (let ai of additionalImports) {
                imports.push(ai);
            }
        }

        if (additionalProviders) {
            for (let ap of additionalProviders) {
                providers.push(ap);
            }
        }

        TestBed.configureTestingModule({
            declarations: declarations,
            imports: imports,
            providers: providers
        }).compileComponents();

        this.fixture = TestBed.createComponent(component);
        this.instance = this.fixture.componentInstance;
        this.element = this.fixture.nativeElement;
        this.fixture.detectChanges();
        this.testBed = TestBed;

        //console.info('xxxxxxx instance main.spec', this.instance.name);
    }

    destroy() {
        Array.from(document.querySelectorAll('style')).forEach(x => x.parentNode.removeChild(x));
        this.fixture.destroy();
        this.instance = null;
    }
}