using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InfraInterfaces = ES.Infrastructure.Interfaces;

namespace ES.Infrastructure.Model;

public class PersonRow : EntityBase, InfraInterfaces.IRow
{
    [Required]
    [Column(TypeName = "varchar(20)")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    public string Surname { get; set; }

    [Required]
    [Column(TypeName = "varchar(9)")]
    public string PassportNumber { get; set; }

    public string PhoneNumber { get; set; }
}
