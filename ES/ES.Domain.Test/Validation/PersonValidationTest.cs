using Microsoft.VisualStudio.TestTools.UnitTesting;
using ES.Domain.Validation;
using System;

namespace ES.Domain.Test.Validation
{
    [TestClass]
    public class PersonValidationTest
    {
        [TestMethod]
        [DataRow(" ","The passport number ( ) length must be 9 and not 1.")]
        [DataRow("abc","The passport number (abc) length must be 9 and not 3.")]
        [DataRow("p123","The passport number (p123) length must be 9 and not 4.")]
        [DataRow("a12345673asda","The passport number (a12345673asda) length must be 9 and not 13.")]
        [DataRow("a12345673","The passport first character  (a) has to be p, P, L or l")]
        [DataRow("p12345673","The passport second character  (1) needs to be a letter")]
        [DataRow("pa123456b","The passport last 7 characters (123456b) needs to be only numbers")]        
        public void WhenHavingAnInvalidPassportNumber_ReturnException(string passportNumber, string errorMessage)
        {
            //Arrange
            
            //Act
            try
            {
                passportNumber.ValidatePassportNumber();
            }
            catch (Exception ex)
            {
                if(ex.Message != errorMessage) 
                    throw ex;

                //Assert
                Assert.IsTrue(ex.Message == errorMessage);
            }
        }

        [TestMethod]
        [DataRow("pl1234567")]
        [DataRow("lk7654321")]
        [DataRow("Lk7654321")]        
        [DataRow("PL7654320")]
        public void WhenHavingAValidPassportNumber_ReturnPassport(string passportNumber)
        {
            //Arrange
            
            //Act
            var result = passportNumber.ValidatePassportNumber();

            //Assert
            Assert.IsTrue(result == passportNumber);
        }
    }
}