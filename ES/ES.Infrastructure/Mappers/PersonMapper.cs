using InfraModel = ES.Infrastructure.Model;
using DomainModel = ES.Domain.Entities;
using ES.Domain.Validation;

namespace ES.Infrastructure.Mappers;

public static class PersonMapper
{
    public static DomainModel.Person ToDomain(this InfraModel.PersonRow person)
    {
        return new ES.Domain.Entities.Person
        {
            Id = person.Id,
            Name = person.Name,
            Surname = person.Surname,
            PassportNumber = person.PassportNumber,
            PhoneNumber = person.PhoneNumber
        };
    }

    public static InfraModel.PersonRow ToInfrastructure(this DomainModel.Person person, IList<string> ExceptionDetails)
    {
        try
        {    
            return new ES.Infrastructure.Model.PersonRow
            {
                Id = person.Id,
                Name = person.Name.ValidateMandatoryFields("Name"),
                Surname = person.Surname.ValidateMandatoryFields("Surname"),
                PassportNumber = person.PassportNumber.ValidateMandatoryFields("PassportNumber").ValidatePassportNumber(),
                PhoneNumber = person.PhoneNumber
            };
        }
        catch(Exception ex)
        {
            ExceptionDetails.Add("Invalid Person details!");
            ExceptionDetails.Add($"Please correct the Validation Error: {ex.Message}");
            return null;
        }
    }
}
