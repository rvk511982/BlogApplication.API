using BlogApp.API.Models.DTOs.User;
using FluentAssertions;

namespace BlogApp.API.Tests.Models
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// Test class for writting unit tests for model
    /// </summary>
    public class AddUserDtoTest
    {
        string username = "test";

        string emailAddress = "test@gmail.com";

        string firstName = "Test";

        string lastName = "hhh";

        string hashedPassword = "www@@@!!!QQQ";

        DateTime createdDate = DateTime.Now;

        [Fact]
        public void AddUserDto_ShouldReturn_Ok()
        {
            var adduser = MockUserData();

            Assert.Equal(adduser.EmailAddress, emailAddress);
        }

        [Fact]
        public void AddUser_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var adduser = MockUserData();

            // Assert
            adduser.Id.Should().Be(1);
            adduser.Username.Should().Be(username);
            adduser.EmailAddress.Should().Be(emailAddress);
            adduser.FirstName.Should().Be(firstName);
            adduser.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1)); // Allow small time variance
            adduser.LastName.Should().Be(lastName);
        }

        [Fact]
        public void User_ShouldHaveDefaultValues()
        {
            // Arrange & Act
            var user = new AddUserDto();
            user.CreatedDate = createdDate;

            // Assert
            user.Id.Should().Be(0); // Default int value
            user.Username.Should().BeEmpty(); // Default string value
            user.EmailAddress.Should().BeEmpty();
            user.FirstName.Should().BeEmpty();
            user.CreatedDate.Should().BeCloseTo(createdDate, TimeSpan.FromSeconds(1)); // Default UTC now
            user.LastName.Should().BeEmpty(); // Default collection
        }

        [Fact]
        public void User_ShouldAllowNullFirstName()
        {
            // Arrange & Act
            var user = new AddUserDto { FirstName = null };

            // Assert
            user.FirstName.Should().BeNull();
        }

        [Fact]
        public void User_ShouldAllowEmptyUserName()
        {
            // Arrange & Act
            var user = new AddUserDto { Username = string.Empty };

            // Assert
            user.Username.Should().BeEmpty();
        }

        [Fact]
        public void User_ShouldAcceptCustomCreatedDate()
        {
            // Arrange
            var customDate = new DateTime(2000, 1, 1);

            // Act
            var user = new AddUserDto { CreatedDate = customDate };

            // Assert
            user.CreatedDate.Should().Be(customDate);
        }

        private AddUserDto MockUserData()
        {
            return new AddUserDto
            {
                Id = 1,
                Username = username,
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName = lastName,
                HashedPassword = hashedPassword,
                CreatedDate = createdDate
            };
        }
    }
}
