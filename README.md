# iShop Web API
API endpoints for creating an simple ecommerce application. The API provides authentication and authorization through JWT, it also provides logging and some security techniques.

# Technology
* ASP.NET Core Web API
* ASP.NET Core Identity
* JWT
* Role based Authorization

# Project structure
  * ## Common
    Common extensions, exceptions, helpers used around the app
  
  * ## Data
    Model entities for the entity framework code first
  
  * ## Repo
    Data persistence of the application, provides how to configure entities, unit of work and how to work with data
    
  * ## Services
    Services for the application, DTOs and mapping
    
  * Web
    Main container of the application, provides API settings and API endpoints
 
 # Installation
  * ## Required:
    * At least .NET core 2.0
  * .NET Core CLI
     * `dotnet ef update database`
     * `dotnet watch run`
  
