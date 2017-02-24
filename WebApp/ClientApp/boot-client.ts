import 'angular2-universal-polyfills/browser'; // This needs to be at the top, Universal neccessary polyfills
import './_workaround.universal.ts'; // temporary until 2.1.1 things are patched in Core

import { enableProdMode } from '@angular/core';
import { platformUniversalDynamic } from 'angular2-universal';
import { AppModule } from './app/app.module';
import 'bootstrap';

// Enable either Hot Module Reloading or production mode
if (module['hot']) {
    module['hot'].accept();
    module['hot'].dispose(() => { platform.destroy(); });
} else {
    enableProdMode();
}

// Boot the application, either now or when the DOM content is loaded
const platform = platformUniversalDynamic();
const bootApplication = () => { platform.bootstrapModule(AppModule); };
if (document.readyState === 'complete') {
    bootApplication();
} else {
    document.addEventListener('DOMContentLoaded', bootApplication);
}
