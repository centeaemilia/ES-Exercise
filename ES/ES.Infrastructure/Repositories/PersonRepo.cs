using DomainModel = ES.Domain.Entities;
using DomainInterfaces = ES.Domain.Interfaces;
using DomainUtils = ES.Domain.Utils;
using InfraModel = ES.Infrastructure.Model;
using ES.Infrastructure.Mappers;

namespace ES.Infrastructure.Repositories;

public class PersonRepo : DomainInterfaces.IPersonRepository
{
    public IList<string> ExceptionDetails {get; private set;}
    public bool HasExceptions 
    {
        get { return ExceptionDetails.Any(); }
    }

    public PersonRepo()
    {
        this.ExceptionDetails = new List<string>();
    }

    public IList<DomainModel.Person> Load()
    {
        PersonContext context = new PersonContext();

        IList<DomainModel.Person> persons = new List<DomainModel.Person>();
        persons = context.Persons.Select(n=>n.ToDomain()).ToList();
 
        return persons;
    }

    public async Task<int> Save(DomainUtils.CRUDEnum action, DomainModel.Person? person, int? personId=null)
    {
        PersonContext context = new PersonContext();

        switch (action)
        {
            case DomainUtils.CRUDEnum.Insert:{
                if(person is not null)
                    return await Insert(context, person);  
                
                ExceptionDetails.Add("The person cannot be null in the insert operation");
                return -1;       
            }
            case DomainUtils.CRUDEnum.Update: {
                if(person is not null && personId.HasValue)
                    return await Update(context, person, personId.Value);  
                
                ExceptionDetails.Add("The person and personId cannot be null in the update operation");
                return -1;       
            }
            case DomainUtils.CRUDEnum.Delete:{ 
                if(personId.HasValue)    
                    return await Delete(context, personId.Value);
                
                ExceptionDetails.Add("The personId cannot be null in the delete operation");
                return -1;
            }
            default: {
                ExceptionDetails.Add("Save action unknown! Only Insert, Update and Delete are supported!");
                return -1;
            }
        }
    }

    public async Task<int> Insert(PersonContext context, DomainModel.Person person)
    {   
        var personInDb = await context.FindPerson(person.Id);
        if (personInDb is not null) 
        {
            ExceptionDetails.Add($"The person with id {person.Id} is already in database. No insert can be performed.");
            return -1;
        }

        var personRow = person.ToInfrastructure(ExceptionDetails);
        if(personRow is null)
            return -1;
        
        context.Persons.Add(personRow);

        return await context.SavePersonContext(ExceptionDetails);
    }

    private async Task<int> Update(PersonContext context, DomainModel.Person personParam, int personId)
    {      
        var person = await context.FindPerson(personId);
        if (person is null) 
        {
            ExceptionDetails.Add($"The person with id {personId} was not found. No update can be performed.");
            return -1;
        }

        var personRow = personParam.ToInfrastructure(ExceptionDetails);
        if(personRow is null)
            return -1;

        person.Name = personRow.Name;
        person.Surname = personRow.Surname;
        person.PassportNumber = personRow.PassportNumber;
        person.PhoneNumber = personRow.PhoneNumber;
        
        return await context.SavePersonContext(ExceptionDetails);
    }
    
    private async Task<int> Delete(PersonContext context, int personId)
    {
        if (await context.FindPerson(personId) is InfraModel.PersonRow  person)
        {
            context.Persons.Remove(person);
            return await context.SavePersonContext(ExceptionDetails);
        }

        ExceptionDetails.Add($"The person with id {personId} was not found. No delete can be performed.");
        return -1;
    }   
}