# ES.Infrastructure.Test

## Notes
Next to do is refactor the Infrastructure in order to be able to use Mock easier and prepare for a more generic approach.
I had to make the 2 context methos virtual in order to test the Insert using mock easier and faster.
More tests on all methods will be needed.

The test in here and in the Domain.Test project are not enough and is work in progress to extend it to cover all cases.
As the DDD architecture was new, I couldn't start with the test projects in parallel as it took me some time and move logic arround while reading DDD documentation and understanding the concepts. So I started with the simple Load and Insert methods and used Postman to test and understand and see I have an end to end flow to the database. Than I created the test project for the insert and finished the other 2 methos and the validation.
By the time I had that, the time spend understanding and creating the projects was much more than 2h so I decided to leave it to the basics and add this notes instead of having a good test coverage and a generig approach.