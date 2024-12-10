using BlogApp.API.Controllers;
using BlogApp.API.Models.DTOs.BlogComments;
using BlogApp.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogApp.API.Tests.Controllers
{
    public class BlogCommentsControllerTest
    {
        private Mock<IBlogCommentService> _mockDataService;
        private Mock<ILogger<BlogCommentsController>> _mockLogger;
        private BlogCommentsController _controller;

        private void InitializeServices()
        {
            _mockDataService = new Mock<IBlogCommentService>();
            _mockLogger = new Mock<ILogger<BlogCommentsController>>();
            _controller = new BlogCommentsController(_mockLogger.Object, _mockDataService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfBlogs()
        {
            InitializeServices();

            // Arrange
            var blogs = MockBlogCommentList();

            _mockDataService.Setup(service => service.GetAllBlogCommentAsync()).ReturnsAsync(blogs);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BlogCommentsDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound()
        {
            InitializeServices();

            // Arrange
            var blogs = new List<BlogCommentsDto>();

            _mockDataService.Setup(service => service.GetAllBlogCommentAsync()).ReturnsAsync(blogs);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BlogCommentsDto>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithBlog()
        {
            InitializeServices();

            // Arrange
            var user = MockBlogCommentData();
            _mockDataService.Setup(service => service.GetBlogCommentByIdAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddBlogCommentDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetById_NotFoundResult_WithBlog()
        {
            InitializeServices();

            // Arrange
            var user = new AddBlogCommentDto();
            _mockDataService.Setup(service => service.GetBlogCommentByIdAsync(9)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetById(9);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddBlogCommentDto>(okResult.Value);
            Assert.Equal(0, returnValue.Id);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WithCreatedBlog()
        {
            InitializeServices();

            // Arrange
            var blogDto = new AddBlogCommentDto { Description = "Test blog" };
            var createdBlog = MockBlogCommentData();
            _mockDataService.Setup(service => service.CreateBlogCommentAsync(blogDto)).ReturnsAsync(createdBlog);

            // Act
            var result = await _controller.Create(blogDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddBlogCommentDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedBlog()
        {
            InitializeServices();

            // Arrange
            var request = MockUpdateBlogCommentDto();
            _mockDataService.Setup(service => service.UpdateBlogCommentAsync(request)).ReturnsAsync(request);

            // Act
            var result = await _controller.Update(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UpdateBlogCommentDto>(okResult.Value);
            Assert.Equal("My blog My name", returnValue.Description);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WithSuccessMessage()
        {
            InitializeServices();

            // Arrange
            _mockDataService.Setup(service => service.DeleteBlogCommentAsync(1)).ReturnsAsync(1);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            Assert.Equal("Blog comment data deleted.", returnValue?.GetType().GetProperty("message").GetValue(returnValue, null));
        }

        private IList<BlogCommentsDto> MockBlogCommentList()
        {
            return new List<BlogCommentsDto> {
                new BlogCommentsDto
                {
                    Id = 1,
                    Description = "My blog My name",
                    BlogId = 12,
                    CreatedDate = DateTime.Now
                }
            };
        }

        private AddBlogCommentDto MockBlogCommentData()
        {
            return new AddBlogCommentDto
            {
                Id = 1,
                Description = "My blog My name",
                BlogId = 12,
                CreatedDate = DateTime.Now
            };
        }

        private UpdateBlogCommentDto MockUpdateBlogCommentDto()
        {
            return new UpdateBlogCommentDto
            {
                Id = 1,
                UpdatedDate = DateTime.UtcNow,
                Description = "My blog My name",
                BlogId = 12
            };
        }
    }
}
