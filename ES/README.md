# ES structure and dependencies
Tools used: Visual Studio Code with CLI commands for making the project, manage dependencies and build
There is one solution with 3 projects and 2 test projects  

## ES overview
The ES project is done following the DDD architecture as I read from: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice
The ES includes 3 projects:
	- ES.API containing the simple webApi with Swagger. 
    ES.API has references for the Domain and Infrastructure
	- ES.Domain is a class library that contains the Entities used by the API, the validation, and others utils classes
	ES.Domain has no reference to other projects or dependencies
	- ES.Infrastructure has references to EF Core SQLite and to the Domain.
	The db file is saved on the disk at a specific location C:\SqLiteDB set up in the Context class.
    For migration I used the commands from https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

I consider the project, as being small, to be self explanatory and the methods name too, so I didn't add any comments.
I find as a good practice to have smaller classes with a specific purpose and the methods to be small and where I see duplicate code, to extract methods.

As part of the next step I also consider creating and using config files, and as a first configuration, to add the database path location. 

## Personal Notes
From this project, all requirements were new to me, as in I didn't work with it before, just some of them read or seen is other projects used or in presentations.
DDD architecture was totally new and I may not yet master it as I found some confusing documentation of what we can put and use in Domain. Some were saying you can have a reference to infrastructure, some were against it. I choose to follow the one from microsoft documentation and stick with it as better as I can understand it in this short time, in order to do this excercise.
The Entity Frameowork In memory and SQlite i didn't encounter them previously before either. I still have questions on how I can use more restrictions and make the PersonRow model more strong, and I will continue this on my own. Having this done now will have taken more time and I consider was not the purpose of this exercise. 