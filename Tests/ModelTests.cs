using System;
using Xunit;
using Models;

namespace Tests
{
    public class ModelTests
    {
        [Fact]
        public void UserShouldSetValidData()
        {
            //Arrange
            User test = new User();
            string testName = "test storefront";

            //Act
            test.Name =testName;

            //Assert
            Assert.Equal(testName, test.Name);

        }

        [Theory]
        [InlineData("")]
        [InlineData("$#^%#$")]
        public void UserShouldNotAllowInvalidName(string input)
        {
            //Arrange
            User test = new User();

            //Act & Assert
            Assert.Throws<InvalidUserNameException>(() => test.Name = input);

        }

        [Theory]
        [InlineData("nNotarealemail#gmail.com")]
        [InlineData("@fantasyemail@gmail.com")]
        [InlineData("endingwithanatsign@yahoo@")]
        public void EmailShouldNotAllowInvalidName(string input)
        {
        //Given
            User test = new User();
            string testEmail = input;
        
            Assert.Throws<EmailVerificationException>(()=>test.Email = testEmail);
        }

        [Fact]
        public void EmailShouldAcceptValidEmail()
        {
        
            User test = new User();
            string testEmail = "justinpeifer@revature.com";
            test.Email = testEmail;
            Assert.Equal(testEmail, test.Email);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(25)]
        public void AgeShouldTakeNumbers(int? input)
        {
        //Given
            User test = new User();
            test.Age =input;
        
        //Then
            Assert.Equal(test.Age, input);
        }
        [Fact]
        public void InventoryCannotBeNegative()
        {
        
            Inventory test = new Inventory();

            int inventoryQuantity = -5;
            
            Assert.Throws<NegativeInventoryException>(()=>test.Quantity = inventoryQuantity);
        
        //Then
        }

        [Theory]
        [InlineData(0)]
        [InlineData(205454563)]
        public void InventoryCanAcceptNonnegativeValues(int? value)
        {
        
            Inventory test = new Inventory();
            test.Quantity = value;
            Assert.Equal(test.Quantity, value);
        
        //Then
        }
    }
}