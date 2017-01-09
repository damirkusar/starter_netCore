# Angular2Core is a seed for Angular2 and Asp.Net Core applications

This seed is an extention of Steve Sanderson´s [ASP.NET Core + Angular 2 template for Visual Studio](http://blog.stevensanderson.com/2016/10/04/angular2-template-for-visual-studio).

It includes [Asp.Net Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) and 
token based authentication with [OpenIdDict](https://github.com/openiddict/openiddict-core)


Here is Steve Sanderson´s [README](https://github.com/damirkusar/Angular2CoreSeed/blob/master/Angular2Core/README.md) of the template.

## Getting Started

Do be able to compile the styles in wwwroot/dist/styles with VS, you will need to install the VS extention "Web Compiler".

Also, when starting the first time, adding new 3rd party libraries, you will need to execute <webpack --config webpack.config.vendor.js> in the project. You will need to have webpack installed globally <npm install -g webpack>

If you update Node for example, and you have already installed node_modules, you will need to rebuild it with <npm rebuild>