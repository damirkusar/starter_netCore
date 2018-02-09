# Bootstrap your .net core backend with Starter_netCore quickly

This starter project gives you a lot of things you may need when starting your project. 
It is split into different projects and services which are doing just that what they need to. Of course you can put everything into one, if you wish. 
The aim was, to split it out and have a flexible architecture (microservices). 

As usuall, everybody does that differently, let me know your toughts and possible changes. 

## Structure

- WebApiGateway: Meant to be the entry point for web apps, you can have multiple gateways if you wish for various clients like native, iot and so on.. but not mandatory
- IdentityProvider: API for your authentication and authorisation, currently running seperately, not used in ApiGateway
- ResourceTemplateProvider: API for your resources. You can have one or multiple API for that. And when the IdentityProvider is running, authorisation works correctly. 

- ResourceTemplate or Identity: Implements the interface project. Kinda a service project to have everything clean and separated. 
- *.Interface: Project for your interfaces
- *.Data: Project for your Database stuff


## Todo

- Docker
- Testing
