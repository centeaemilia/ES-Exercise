# ES.Infrastructure

## Notes
Next is to see how i can move more logic from the Infrastructure. 
I would like to make the Context generic, not dependent on the PersonRow. Use generic types.
Create a generic abstract Repository and be able to easily add a new table if needed, withouth duplicating the context and PersonRepo classes.

I created the PersonRow and the Person models separately as I used in the PersonRow the EF attributes so I couldn't put it in the Domain. Also I consider it better to have the API model separately from the Infrastructure model as this helps in case the Infrastructure model evolves in a different speed as the other. So having the mapping helps control this and
keeps the project functional while new columns are added or types are changed.

For now I chosed to leave it like this as is no need for a generic class when you only have one table. 
When the second one will be requested, we should not duplicate, but refactor and implement the generic approach.
For the purpose of this excercises I wanted to use the clean code approach and understand the DDD architecture, see how EF works, and leavind the Domain clean of any dependency.  