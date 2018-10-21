# CookieBook-WebAPI
This is a WebAPI for Cookie Book project. This project is a cookbook system where users can add, share and rate other's culinary recipes.

It's responsible for the server side of the project: storing, modifying and returning data.
It follows the rules of the RESTfull API.

## Table of contents
* [Technologies](#technologies)
* [Launch](#launch)
* [Scope of functionalities](#scope-of-functionalities)
* [Project status](#project-status)

## Technologies
* [.NET Core 2.1](https://www.microsoft.com/net/download)
* [Entity Framework Core 2.1.1](https://docs.microsoft.com/en-us/ef/core/)
* [Microsoft SQL Server 2017](https://www.microsoft.com/en-us/sql-server/sql-server-2017)
* [Fluent Validation 8.0.0-preview3](https://fluentvalidation.net/)

## Launch
1. Download this project/repository.
2. Download and install:
   * [.NET Core 2.1 Runtime](https://www.microsoft.com/net/download)
   * [Microsoft SQL Server 2017](https://www.microsoft.com/en-us/sql-server/sql-server-2017)
3. Go to the `CookieBook-WebAPI\CookieBook.WebAPI` and run `Powershell/Command Promp/Terminal` in this location.
4. Type `dotnet ef database update`. This will create the database and apply all migrations.
5. Type `dotnet run`. This will run the WebAPI.
6. You can now work with this WebAPI.

## Scope of functionalities
<table>
  <tr>
    <th>Functionality</th>
    <th>Status</th>
  </tr>
  
  <tr>
    <td>User registration</td>
    <td>:heavy_check_mark:</td>
  </tr>
  
  <tr>
    <td>User loging in</td>
    <td>:heavy_check_mark:</td>
  </tr>
  
  <tr>
    <td>Updating user data</td>
    <td>:heavy_check_mark:</td>
  </tr>
  
  <tr>
    <td>Changing user password</td>
    <td>:heavy_check_mark:</td>
  </tr>
  
  <tr>
    <td>Adding cooking recipe</td>
    <td>:construction:</td>
  </tr>
    
  <tr>
    <td>Updating cooking recipe</td>
    <td>:construction:</td>
  </tr>
  
  <tr>
    <td>Commenting cooking recipes</td>
    <td>:construction:</td>
  </tr>
  
  <tr>
    <td>Cooking recipes ratting</td>
    <td>:construction:</td>
  </tr>
  
  <tr>
    <td>...</td>
    <td>...</td>
  </tr>
</table>

## Project status
Current status: WORK IN PROGRESS :construction:

Application is before it's first release.

A `Master` branch is always the stable one. If you are intrested in testing the project in its current form then you should download this branch.
