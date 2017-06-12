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

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';

import { HttpErrorHandlerService } from './core/services/http-error-handler.service';
import { HttpOptionsService } from './core/services/http-options.service';
import { AuthGuardService } from './core/services/auth-guard.service';
import { CanDeactivateGuardService } from './core/services/can-deactivate-guard.service';

import { AccountService } from './core/services/account.service';
import { AuthService } from './core/services/auth.service';
import { LoggerService } from './core/services/logger.service';
import { LoaderService } from './core/services/loader.service';

import { FilterPipe } from './shared/pipes/filter.pipe';

export class MainSpec {
    fixture: any;
    instance: any;
    element: any;
    testBed: any;
    constructor() { }

    init(component, additionalDeclarations, additionalImports, additionalProviders): void {
        if (!additionalImports) {
            additionalImports = [];
        }
        additionalImports.push(SharedModule);

        this.initWithoutSharedModule(component, additionalDeclarations, additionalImports, additionalProviders);
    }

    initForSharedComponents(component, additionalDeclarations, additionalImports, additionalProviders): void {
        if (!additionalDeclarations) {
            additionalDeclarations = [];
        }
        additionalDeclarations = additionalDeclarations.concat([FilterPipe]);

        if (!additionalImports) {
            additionalImports = [];
        }
        additionalImports = additionalImports.concat([]);

        if (!additionalProviders) {
            additionalProviders = [];
        }
        additionalProviders = additionalProviders.concat(
            []
        );

        this.initWithoutSharedModule(component, additionalDeclarations, additionalImports, additionalProviders);
    }

    private initWithoutSharedModule(component, additionalDeclarations, additionalImports, additionalProviders): void {
        console.info('Initalizing Tests for', component.name);
        let declarations = [component];

        let imports = [
            CoreModule,
            BrowserAnimationsModule,
            BrowserModule,
            HttpModule,
            JsonpModule,
            FormsModule,
            LocalStorageModule.withConfig({
                prefix: 'angularXcore-test',
                storageType: 'localStorage'
            }),
            LocalizationModule.forRoot(),
            RouterModule.forRoot([])
        ];

        let providers = [
            { provide: APP_BASE_HREF, useValue: '/' }
        ];

        //console.info('additionalDeclarations', additionalDeclarations);
        //console.info('additionalImports', additionalImports);
        //console.info('additionalProviders', additionalProviders);

        if (additionalDeclarations) {
            declarations = declarations.concat(additionalDeclarations);
        }

        if (additionalImports) {
            imports = imports.concat(additionalImports);
        }

        if (additionalProviders) {
            providers = providers.concat(additionalProviders);
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