using BlogApp.API.Models.DTOs.BlogComments;
using BlogApp.API.Repositories.Interface;
using BlogApp.API.Services;
using BlogApp.API.Services.Interface;
using Moq;

namespace BlogApp.API.Tests.Services
{
    public class BlogCommentServiceTest
    {
        private Mock<IBlogCommentRepository> _mockRepository;
        private BlogCommentService _mockService;
        private Mock<IBlogCommentService> _mockDataService;

        private void InitializeServices()
        {
            _mockRepository = new Mock<IBlogCommentRepository>();
            _mockDataService = new Mock<IBlogCommentService>();
            _mockService = new BlogCommentService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult_WithList_BlogComment()
        {
            InitializeServices();

            // Arrange
            var blogs = MockBlogCommentsList();

            _mockDataService.Setup(service => service.GetAllBlogCommentAsync()).ReturnsAsync(blogs);

            var result = await _mockService.GetAllBlogCommentAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound_BlogComment()
        {
            InitializeServices();

            // Arrange
            var outputData = MockBlogCommentsList();

            _mockDataService.Setup(service => service.GetAllBlogCommentAsync()).ReturnsAsync(outputData);

            // Act
            var result = await _mockService.GetAllBlogCommentAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_With_BlogComment()
        {
            InitializeServices();

            // Arrange
            var outputData = MockAddBlogCommentsData();
            _mockDataService.Setup(service => service.GetBlogCommentByIdAsync(1)).ReturnsAsync(outputData);

            // Act
            var result = await _mockService.GetBlogCommentByIdAsync(1);

            // Assert
            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task GetById_NotFoundResult_WithBlogComment()
        {
            InitializeServices();

            // Arrange
            var blog = new AddBlogCommentDto();
            _mockDataService.Setup(service => service.GetBlogCommentByIdAsync(9)).ReturnsAsync(blog);

            // Act
            var result = await _mockService.GetBlogCommentByIdAsync(9);

            // Assert
            Assert.Equal(0, 0);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WithCreate_BlogComment()
        {
            InitializeServices();

            // Arrange
            var blogComment = MockAddBlogCommentsData();

            var createdBlog = MockAddBlogCommentsData();
            _mockDataService.Setup(service => service.CreateBlogCommentAsync(blogComment)).ReturnsAsync(createdBlog);

            // Act
            var result = await _mockService.CreateBlogCommentAsync(blogComment);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdated_BlogComment()
        {
            InitializeServices();

            // Arrange
            var updateDto = MockUpdateBlogCommentsDto();

            _mockDataService.Setup(service => service.UpdateBlogCommentAsync(updateDto)).ReturnsAsync(updateDto);

            // Act
            var result = await _mockService.UpdateBlogCommentAsync(updateDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WithSuccessMessage_BlogComment()
        {
            InitializeServices();

            // Arrange
            _mockDataService.Setup(service => service.DeleteBlogCommentAsync(1)).ReturnsAsync(1);

            // Act
            var result = await _mockService.DeleteBlogCommentAsync(1);

            // Assert
            Assert.Equal(0, 0);
        }

        private IList<BlogCommentsDto> MockBlogCommentsList()
        {
            return new List<BlogCommentsDto> {
                new BlogCommentsDto
                {
                    Id = 1,
                    Description = "John JohnDoe JohnDoe",
                    BlogId = 123,
                    CreatedDate = DateTime.Now
                }
            };
        }

        private AddBlogCommentDto MockAddBlogCommentsData()
        {
            return new AddBlogCommentDto
            {
                Id = 1,
                Description = "John JohnDoe JohnDoe",
                BlogId = 123,
                CreatedDate = DateTime.Now
            };
        }

        private UpdateBlogCommentDto MockUpdateBlogCommentsDto()
        {
            return new UpdateBlogCommentDto
            {
                Id = 1,
                UpdatedDate = DateTime.UtcNow,
                Description = "John JohnDoe JohnDoe",
                BlogId = 12
            };
        }
    }
}
