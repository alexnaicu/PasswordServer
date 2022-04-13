# PasswordServer
An API for generating temporary passwords

The targeted framework is .NET5

The solution contains:
+ an Web API project 
  - provides an endpoint "/api/passwords" for generating passwords with a POST message containing a json with the UserId field
  - is configurable via appsettings.json
  - uses a sql database to store password data
+ an Web App project 
  - simple client for visualizing results from the Web API
+ two Tests projects 
  - a unit test project and an integration test project for the Web API
