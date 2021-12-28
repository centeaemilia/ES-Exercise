# ES-Exercise
Simple .Net Core CRUD WebApi 

## Objective
Create a project in ASP.net Core (C#) following the DDD pattern. Implement a simple CRUD API and use an API client generator (like Swagger) to demonstrate it's functionality.

## Details
- The project doesn't need authentication or authorization, only a simple CRUD application to register people with name, surname, passport number and phone. 
- The name, surname and passport number will be mandatory.  The validation for for the passport number must check format and avoid invalid passport numbers being collected. 
- Passport number format expected will begin with the letter P or L and will be followed by another letter and 7 numbers.
- The list can be simple - a table with name, surname and passport number, and the actions to edit and delete.
- The persistence should be with EF Core. Preferably Sqlite, although you can also use in-memory persistence.
- Use, whenever you can, the asynchronous methods of EF for the operations with the database.

## Tehnical Notes
Unit testing is important. Code should be testable. For now, demonstrate the tests you consider necessary to cover the core logic of the application. 
Using mocks will be evaluated in positive way. 

Feel free to make decisions on implementation, just provide a reason for doing so in a readme. 
We are fine with you using a framwork or boiler plate, just provide a reason for doing so in a readme. 
Finally, outline next steps or anything you didn't finish in a readme. 
