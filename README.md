# Angular2Core is a seed for Angular2 and Asp.Net Core applications

This seed is an extention of Steve Sanderson´s [ASP.NET Core + Angular 2 template for Visual Studio](http://blog.stevensanderson.com/2016/10/04/angular2-template-for-visual-studio).

It includes [Asp.Net Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) and 
token based authentication with [OpenIdDict](https://github.com/openiddict/openiddict-core).

It also includes a starting point with a login mechanism and auto logout when token expires. It also uses scss instead of css. 

Localization is also included with the following librarie: [angular2localization](https://github.com/robisim74/angular2localization)

Here is Steve Sanderson´s [README](https://github.com/damirkusar/Angular2CoreSeed/blob/master/Angular2Core/README.md) of the template.

# Getting Started

Do be able to compile the styles in wwwroot/dist/styles with VS, you will need to install the VS extention "Web Compiler".

Also, when starting the first time, adding new 3rd party libraries, you will need to execute <webpack --config webpack.config.vendor.js> in the project. You will need to have webpack installed globally <npm install -g webpack>

If you update Node for example, and you have already installed node_modules, you will need to rebuild it with <npm rebuild>

## Needed Software
You will also need [nodeJS](https://nodejs.org/en/) installed and [.net core tools](https://www.microsoft.com/net/core#windowsvs2015) with [.net core 1.1](https://www.microsoft.com/net/download/core)

# Hints
## Lazy Loading

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