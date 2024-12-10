using BlogApp.API.Controllers;
using BlogApp.API.Models.DTOs.BlogPost;
using BlogApp.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogApp.API.Tests.Controllers
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// Controller test class for writing unit tests for controller actions
    /// </summary>
    public class BlogsControllerTest
    {
        private Mock<IBlogService> _mockDataService;
        private Mock<ILogger<BlogsController>> _mockLogger;
        private BlogsController _controller;

        private void InitializeServices()
        {
            _mockDataService = new Mock<IBlogService>();
            _mockLogger = new Mock<ILogger<BlogsController>>();
            _controller = new BlogsController(_mockLogger.Object, _mockDataService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfBlogs()
        {
            InitializeServices();

            // Arrange
            var blogs = MockBlogList();

            _mockDataService.Setup(service => service.GetAllBlogAsync()).ReturnsAsync(blogs);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BlogPostDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound()
        {
            InitializeServices();

            // Arrange
            var blogs = new List<BlogPostDto>();

            _mockDataService.Setup(service => service.GetAllBlogAsync()).ReturnsAsync(blogs);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BlogPostDto>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithBlog()
        {
            InitializeServices();

            // Arrange
            var user = MockBlogData();
            _mockDataService.Setup(service => service.GetBlogByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddBlogDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetById_NotFoundResult_WithBlog()
        {
            InitializeServices();

            // Arrange
            var user = new AddBlogDto();
            _mockDataService.Setup(service => service.GetBlogByIdAsync(9)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetById(9);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddBlogDto>(okResult.Value);
            Assert.Equal(0, returnValue.Id);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WithCreatedBlog()
        {
            InitializeServices();

            // Arrange
            var blogDto = new AddBlogDto { Title = "Test blog" };
            var createdBlog = MockBlogData();
            _mockDataService.Setup(service => service.CreateBlogAsync(blogDto)).ReturnsAsync(createdBlog);

            // Act
            var result = await _controller.Create(blogDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddBlogDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedBlog()
        {
            InitializeServices();

            // Arrange
            var request = MockUpdateBlogDto();
            _mockDataService.Setup(service => service.UpdateBlogAsync(request)).ReturnsAsync(request);

            // Act
            var result = await _controller.Update(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UpdateBlogDto>(okResult.Value);
            Assert.Equal("My blog My name", returnValue.Title);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WithSuccessMessage()
        {
            InitializeServices();

            // Arrange
            _mockDataService.Setup(service => service.DeleteBlogAsync(1)).ReturnsAsync(1);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            Assert.Equal("Blog data deleted.", returnValue?.GetType().GetProperty("message").GetValue(returnValue, null));
        }

        private IList<BlogPostDto> MockBlogList()
        {
            return new List<BlogPostDto> {
                new BlogPostDto
                {
                    Id = 1,
                    Title = "My blog My name",
                    Description = "test@gmail.com",
                    UserId = 123,
                    CreatedDate = DateTime.Now
                }
            };
        }

        private AddBlogDto MockBlogData()
        {
            return new AddBlogDto
            {
                Id = 1,
                Title = "My blog My name",
                Description = "test@gmail.com",
                UserId = 123,
                CreatedDate = DateTime.Now
            };
        }

        private UpdateBlogDto MockUpdateBlogDto()
        {
            return new UpdateBlogDto
            {
                Id = 1,
                Title = "My blog My name",
                UpdatedDate = DateTime.UtcNow,
                Description = "test@gmail.com",
                UserId = 123
            };
        }
    }
}
