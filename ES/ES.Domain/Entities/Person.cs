using ES.Domain.Interfaces;

namespace ES.Domain.Entities;

public class Person : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PassportNumber { get; set; }
    public string PhoneNumber { get; set; }
}
