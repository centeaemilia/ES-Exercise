
using ES.Domain.Entities;
using ES.Domain.Utils;

namespace ES.Domain.Interfaces;

public interface IPersonRepository
{
    IList<string> ExceptionDetails {get;}

    bool HasExceptions {get;}

    IList<Person> Load();

    Task<int> Save(CRUDEnum action, Person? person=null, int? id=null);
}
