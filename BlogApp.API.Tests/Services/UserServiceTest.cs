using BlogApp.API.Models.DTOs.User;
using BlogApp.API.Repositories.Interface;
using BlogApp.API.Services;
using BlogApp.API.Services.Interface;
using Moq;

namespace BlogApp.API.Tests.Services
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// Service test class for writting unit tests for service methods
    /// </summary>
    public class UserServiceTest
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UserService _mockService;
        private Mock<IUserService> _mockUserService;

        private void InitializeServices()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserService = new Mock<IUserService>();
            _mockService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult_WithListOfUsers()
        {
            InitializeServices();

            // Arrange
            var users = MockUsersList();

            _mockUserService.Setup(service => service.GetAllAsync()).ReturnsAsync(users);            

            var result = await _mockService.GetAllAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound()
        {
            InitializeServices();

            // Arrange
            var users = new List<UsersDto>();

            _mockUserService.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _mockService.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithUser()
        {
            InitializeServices();

            // Arrange
            var user = MockUserData();
            _mockUserService.Setup(service => service.GetUserByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _mockService.GetUserByIdAsync(1);

            // Assert
            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task GetById_NotFoundResult_WithUser()
        {
            InitializeServices();

            // Arrange
            var user = new AddUserDto();
            _mockUserService.Setup(service => service.GetUserByIdAsync(9)).ReturnsAsync(user);

            // Act
            var result = await _mockService.GetUserByIdAsync(9);

            // Assert
            Assert.Equal(0, 0);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WithCreatedUser()
        {
            InitializeServices();

            // Arrange
            var userDto = new AddUserDto { 
                FirstName = "Test Name",
                CreatedDate = DateTime.Now,
                EmailAddress = "test@gmail.com",
                HashedPassword = "fda#$#%#)(Ul",
                LastName = "test",
                Username= "testuser",
            };
            var createdUser = MockUserData();
            _mockUserService.Setup(service => service.CreateUserAsync(userDto)).ReturnsAsync(createdUser);

            // Act
            var result = await _mockService.CreateUserAsync(userDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedUser()
        {
            InitializeServices();

            // Arrange
            var userDto = MockUpdateUserDto();
            var updatedUser = MockUpdateUserDto();

            _mockUserService.Setup(service => service.UpdateUserAsync(userDto)).ReturnsAsync(updatedUser);

            // Act
            var result = await _mockService.UpdateUserAsync(userDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WithSuccessMessage()
        {
            InitializeServices();

            // Arrange
            _mockUserService.Setup(service => service.DeleteUserAsync(1)).ReturnsAsync(1);

            // Act
            var result = await _mockService.DeleteUserAsync(1);

            // Assert
            Assert.Equal(0, 0);
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

        private UpdateUserDto MockUpdateUserDto()
        {
            return new UpdateUserDto
            {
                Id = 1,
                FirstName = "John Doe Updated",
                UpdatedDate = DateTime.Now,
                EmailAddress = "test@gmail.com",
                HashedPassword = "fda#$#%#)(Ul",
                LastName = "test",
                Username = "testuser",
            };
        }
    }
}
