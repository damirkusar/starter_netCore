import 'es6-shim';
import 'zone.js';
import 'reflect-metadata';
import 'bootstrap';
import '@angular/animations';
import '@angular/animations/browser';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import 'bootstrap';
import '@angular/animations';
import '@angular/animations/browser';

const rootElemTagName = 'app'; // Update this if you change your root component selector

// Enable either Hot Module Reloading or production mode
if (module['hot']) {
    module['hot'].accept();
    module['hot'].dispose(() => {
        // Before restarting the app, we create a new root element and dispose the old one
        const oldRootElem = document.querySelector(rootElemTagName);
        const newRootElem = document.createElement(rootElemTagName);
        oldRootElem.parentNode.insertBefore(newRootElem, oldRootElem);
        modulePromise.then(appModule => appModule.destroy());
    });
} else {
    enableProdMode();
}

const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);
