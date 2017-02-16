# AngularMeetsNetCore is a seed for Angular and Asp.Net Core applications

This seed is an extention of Steve Sanderson´s [ASP.NET Core + Angular 2 template for Visual Studio](http://blog.stevensanderson.com/2016/10/04/angular2-template-for-visual-studio).

It includes [Asp.Net Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) and 
token based authentication with [OpenIdDict](https://github.com/openiddict/openiddict-core).

It also includes a starting point with a login mechanism and auto logout when token expires. It also uses scss instead of css. 

Localization is also included with the following librarie: [angular-l10n](https://github.com/4vanger/angular-l10n)

Here is Steve Sanderson´s [README](https://github.com/damirkusar/AngularMeetsNetCore/blob/master/WebApp/README.md) of the template.

# Getting Started

## Pre requirements
1. Do be able to compile the styles in wwwroot/dist/styles with VS, you will need to install the VS extention "Web Compiler".
2. You will need to have webpack installed globally <npm install -g webpack>
3. You will also need [nodeJS](https://nodejs.org/en/) installed 
4. You will also need [.net core tools](https://www.microsoft.com/net/core#windowsvs2015) with [.net core 1.1](https://www.microsoft.com/net/download/core) installed

## Running the App for the first time
In the package.json file, there is a script <webpack-vendor> and <webpack> which you have to run. 
If you add a new or you update a package, and you have updated webpack.config.vendor.js file, then run this commands again, manually.

-> If you running into problems, delete first all files under wwwroot/dist (except _placeholder.txt) as well ClientApp/dist (except _placeholder.txt)

In the DataAccessLayer, there are some npm scripts. You will need to run these to setup your DB. You can make changes of course as you like. 
- npm run init-applicationdb
- npm run init-datadb

These commands will create the init migrations and create / update the DB you have specified. 
## Hints
If you update Node for example, and you have already installed node_modules, you will need to rebuild it with <npm rebuild>

# Lazy Loading

This project is prepared for lazy loading of angular modules. 
In your route configuration, use loadChildren with a relative path to your lazy loaded angular module. The string is delimited with a # where the right side of split is the angular module class name.

```TypeScript
import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: 'lazy', loadChildren: './lazy.module#LazyModule' }
];
```

You will also need to remove the UniversalModule from the feature modules since it does not like that BrowserModule gets loaded twice then. But needed when not doing lazy loading. 
Used library: https://github.com/brandonroberts/angular-router-loader