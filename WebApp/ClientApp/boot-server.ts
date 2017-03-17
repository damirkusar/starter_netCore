//import './_workaround.prerendering';
import 'zone.js';
import 'reflect-metadata';
import '@angular/animations';
import '@angular/animations/browser';
import { enableProdMode } from '@angular/core';
import { platformDynamicServer, platformServer } from '@angular/platform-server';

import { createServerRenderer, RenderResult } from 'aspnet-prerendering';

import { AppModule } from './app/app.module';

enableProdMode();
const platform = platformDynamicServer();

export default createServerRenderer(params => {
    return new Promise<RenderResult>((resolve, reject) => {
        const requestZone = Zone.current.fork({
            name: 'angular-universal request',
            properties: {
                ngModule: AppModule,
                baseUrl: '/',
                requestUrl: params.url,
                originUrl: params.origin,
                preboot: false,
                document: '<app></app>'
            },
            onHandleError: (parentZone, currentZone, targetZone, error) => {
                // If any error occurs while rendering the module, reject the whole operation
                reject(error);
                return true;
            }
        });

        return requestZone.run<Promise<string>>(() => platform.bootstrapModule(AppModule))
            .then(html => {
                    resolve({ html: html });
                },
                reject);
    });
})
