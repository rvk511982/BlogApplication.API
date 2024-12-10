using BlogApp.API.Controllers;
using BlogApp.API.Models.DTOs.User;
using BlogApp.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogApp.API.Tests.Controllers
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// Controller test class for writtng unit tests for controller actions
    /// </summary>
    public class UserControllerTest
    {
        private Mock<IUserService> _mockUserService;
        private Mock<ILogger<UserController>> _mockLogger;
        private UserController _controller;

        private void InitializeServices()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockLogger.Object, _mockUserService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfUsers()
        {
            InitializeServices();

            // Arrange
            var users = MockUsersList();

            _mockUserService.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<UsersDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound()
        {
            InitializeServices();

            // Arrange
            var users = new List<UsersDto>();

            _mockUserService.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<UsersDto>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithUser()
        {
            InitializeServices();

            // Arrange
            var user = MockUserData();
            _mockUserService.Setup(service => service.GetUserByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddUserDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetById_NotFoundResult_WithUser()
        {
            InitializeServices();

            // Arrange
            var user = new AddUserDto();
            _mockUserService.Setup(service => service.GetUserByIdAsync(9)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetById(9);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddUserDto>(okResult.Value);
            Assert.Equal(0, returnValue.Id);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WithCreatedUser()
        {
            InitializeServices();

            // Arrange
            var userDto = new AddUserDto { FirstName = "Test Name" };
            var createdUser = MockUserData();
            _mockUserService.Setup(service => service.CreateUserAsync(userDto)).ReturnsAsync(createdUser);

            // Act
            var result = await _controller.Create(userDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddUserDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedUser()
        {
            InitializeServices();

            // Arrange
            var userDto = new UpdateUserDto { Id = 1, FirstName = "John Doe Updated" };
            var updatedUser = new UpdateUserDto { Id = 1, FirstName = "John Doe Updated" };
            _mockUserService.Setup(service => service.UpdateUserAsync(userDto)).ReturnsAsync(updatedUser);

            // Act
            var result = await _controller.Update(userDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UpdateUserDto>(okResult.Value);
            Assert.Equal("John Doe Updated", returnValue.FirstName);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WithSuccessMessage()
        {
            InitializeServices();

            // Arrange
            _mockUserService.Setup(service => service.DeleteUserAsync(1)).ReturnsAsync(1);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            Assert.Equal("User data deleted.", returnValue?.GetType().GetProperty("message").GetValue(returnValue, null));
        }

        private IList<UsersDto> MockUsersList()
        {
            return new List<UsersDto> {
                new UsersDto
                {
                    Id = 1,
                    Username = "JohnDoe" ,
                    EmailAddress = "test@test.com",
                    FirstName = "John",
                    LastName = "Doe",
                    CreatedDate = DateTime.Now
                }
            };
        }

        private AddUserDto MockUserData()
        {
            return new AddUserDto
            {
                Id = 1,
                Username = "JohnDoe",
                EmailAddress = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                CreatedDate = DateTime.Now
            };
        }
    }
}
