using DomainEntities = ES.Domain.Entities;
using ES.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var personService = new PersonService();

app.MapGet("/GetAllPersons", () =>
{ 
    return personService.Load();
})
.WithName("GetAllPersons");

app.MapPost("/InsertPerson", async (DomainEntities.Person person) =>
{
    return await personService.Insert(person);
});

app.MapPut("/UpdatePerson/{id}", async (int id, DomainEntities.Person person) =>
{
    return await personService.Update(person, id);
});

app.MapDelete("/DeletePerson/{id}", async (int id) =>
{
    return await personService.Delete(id);
});

app.Run();
