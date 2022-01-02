using Microsoft.EntityFrameworkCore;
using InfraModel = ES.Infrastructure.Model;
using InfraInterface = ES.Infrastructure.Interfaces;

namespace ES.Infrastructure.Repositories;

public class PersonContext : DbContext, InfraInterface.IDbContext
{
    public DbSet<InfraModel.PersonRow> Persons { get; set; }
    private const string SqLiteDB_Path = "C:\\SqLiteDB";

    public string DbPath { get; }

    public PersonContext()
    {
        this.DbPath = System.IO.Path.Join(SqLiteDB_Path, "person.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");


    public virtual async ValueTask<InfraModel.PersonRow?> FindPerson(int personId)
    {
        return await this.Persons.FindAsync(personId);
    }

    public virtual async Task<int> SavePersonContext(IList<string> ExceptionDetails)
    {
        try
        {
            return await this.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            ExceptionDetails.Add($"The action finish with erros. Error Message: {ex.Message}. Inner message: {ex.StackTrace}");
        }

        return -1;
    }
}
