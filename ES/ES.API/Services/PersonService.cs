using DomainUtils = ES.Domain.Utils;
using DomainEntities = ES.Domain.Entities;
using DomainInterfaces = ES.Domain.Interfaces;
using InfraRepo = ES.Infrastructure.Repositories;

namespace ES.API.Services;

public class PersonService
{
    DomainInterfaces.IPersonRepository personRepo;

    public PersonService()
    {
        personRepo = new InfraRepo.PersonRepo();
    }

    #region CRUD operations
    public IList<DomainEntities.Person> Load()
    {
        ClearPreviousExceptions();
        var persons = personRepo.Load() ;

        return persons;
    }

    public async Task<IResult> Insert(DomainEntities.Person person)
    {
        ClearPreviousExceptions();
        await personRepo.Save(DomainUtils.CRUDEnum.Insert, person: person);
        
        return ReturnResult(person);
    }

    public async Task<IResult> Update(DomainEntities.Person person, int personId)
    {
        ClearPreviousExceptions();
        await personRepo.Save(DomainUtils.CRUDEnum.Update, person: person, id: personId);

        return ReturnResult(personId);
    }

    public async Task<IResult> Delete(int personId)
    {
        ClearPreviousExceptions();
        await personRepo.Save(DomainUtils.CRUDEnum.Delete, id: personId);

        return  ReturnResult(personId);
    }
    #endregion

    #region Private utils methods
    private void ClearPreviousExceptions()
    {
        personRepo.ExceptionDetails.Clear();
    }

    private IResult ReturnResult(object okReturn)
    {
        return personRepo.HasExceptions? 
            Results.Text(string.Join(" ", personRepo.ExceptionDetails.ToArray())) : 
            Results.Ok(okReturn);
    }
    #endregion
}