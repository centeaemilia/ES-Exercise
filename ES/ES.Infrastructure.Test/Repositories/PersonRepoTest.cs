using Microsoft.VisualStudio.TestTools.UnitTesting;
using ES.Infrastructure.Repositories;  
using Moq;
using ES.Domain.Entities;
using System.Threading.Tasks;

namespace ES.Infrastructure.Test.Repositories;

[TestClass]
public class PersonRepoTest
{
    [TestMethod]
    public void WhenInsertAnExistingPerson_ErrorMessageDisplayed()
    {
        //Arrange
        Mock<PersonContext> personContextMock = new Mock<PersonContext>();
        var mockPerson = new Model.PersonRow()
                            {
                                Id = 1,
                                Name = "Emilia",
                                Surname = "Centea",
                                PassportNumber = "pl1234567",
                                PhoneNumber=""
                            };
        
        personContextMock.Setup(p => p.FindPerson(1)).Returns(new ValueTask<Model.PersonRow>(mockPerson));  
        personContextMock.Setup(p => p.SavePersonContext(null)).Returns(new Task<int>(()=> -1) );  

        var persontoInsert = new Person()
                                {
                                    Id=1,
                                    Name = "test",
                                    Surname = "test1",
                                    PassportNumber = "123",
                                    PhoneNumber=""           
                                };

        //Act
        PersonRepo personRepo = new PersonRepo();  
        var result = personRepo.Insert(personContextMock.Object, persontoInsert);  
            
        //Assert
        Assert.IsTrue(personRepo.HasExceptions);
        Assert.AreEqual("The person with id 1 is already in database. No insert can be performed.",string.Join("", personRepo.ExceptionDetails));
    }

    [TestMethod]
    public void WhenInsertAnInvalidPerson_ErrorMessageDisplayed()
    {
        //Arrange
        Mock<PersonContext> personContextMock = new Mock<PersonContext>();
        
        personContextMock.Setup(p => p.FindPerson(1)).Returns(null);  
        personContextMock.Setup(p => p.SavePersonContext(null)).Returns(new Task<int>(()=> -1) );  

        var persontoInsert = new Person()
                                {
                                    Id=1,
                                    Name = "",
                                    Surname = "",
                                    PassportNumber = "",
                                    PhoneNumber=""           
                                };

        //Act for Name field
        PersonRepo personRepo = new PersonRepo();  
        personRepo.Insert(personContextMock.Object, persontoInsert).Wait();  

        //Assert
        Assert.IsTrue(personRepo.HasExceptions);
        var expectedErrormessage = "Invalid Person details!Please correct the Validation Error: The field Name cannot be null, empty or empty spaces. Please fill in with a valid string.";
        Assert.AreEqual(expectedErrormessage, string.Join("", personRepo.ExceptionDetails));


        //Act for Surname field
        persontoInsert.Name = "test";
        personRepo.ExceptionDetails.Clear();
        personRepo.Insert(personContextMock.Object, persontoInsert).Wait();

        //Assert
        Assert.IsTrue(personRepo.HasExceptions);
        expectedErrormessage = "Invalid Person details!Please correct the Validation Error: The field Surname cannot be null, empty or empty spaces. Please fill in with a valid string.";
        Assert.AreEqual(expectedErrormessage, string.Join("", personRepo.ExceptionDetails));

        //Act for Passport field
        persontoInsert.Surname = "test surname";
        personRepo.ExceptionDetails.Clear();
        personRepo.Insert(personContextMock.Object, persontoInsert).Wait();

        //Assert
        Assert.IsTrue(personRepo.HasExceptions);
        expectedErrormessage = "Invalid Person details!Please correct the Validation Error: The field PassportNumber cannot be null, empty or empty spaces. Please fill in with a valid string.";
        Assert.AreEqual(expectedErrormessage, string.Join("", personRepo.ExceptionDetails));

         //Act for Passport field
        persontoInsert.PassportNumber = "123";
        personRepo.ExceptionDetails.Clear();
        personRepo.Insert(personContextMock.Object, persontoInsert).Wait();

        //Assert
        Assert.IsTrue(personRepo.HasExceptions);
        expectedErrormessage = "Invalid Person details!Please correct the Validation Error: The passport number (123) length must be 9 and not 3.";
        Assert.AreEqual(expectedErrormessage, string.Join("", personRepo.ExceptionDetails));
    }

    
    [TestMethod]
    public void WhenInsertAValidPerson_ShouldPass()
    {
        //Arrange
        Mock<PersonContext> personContextMock = new Mock<PersonContext>();
        
        personContextMock.Setup(p => p.FindPerson(1)).Returns(null);  
        var resultMock = new Task<int>(()=> 1);
        personContextMock.Setup(p => p.SavePersonContext(null)).Returns( resultMock);  

        var persontoInsert = new Person()
                                {
                                    Id=1,
                                    Name = "Emilia",
                                    Surname = "Centea",
                                    PassportNumber = "pk1234567",
                                    PhoneNumber=""           
                                };

        //Act for Name field
        PersonRepo personRepo = new PersonRepo();  
        personRepo.Insert(personContextMock.Object, persontoInsert);

        //Assert
        Assert.IsFalse(personRepo.HasExceptions);
        Assert.AreEqual(string.Empty, string.Join("", personRepo.ExceptionDetails));
   }
}