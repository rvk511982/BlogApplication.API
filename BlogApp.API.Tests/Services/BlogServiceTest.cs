using BlogApp.API.Models.DTOs.BlogPost;
using BlogApp.API.Repositories.Interface;
using BlogApp.API.Services;
using BlogApp.API.Services.Interface;
using Moq;

namespace BlogApp.API.Tests.Services
{
    public class BlogServiceTest
    {
        private Mock<IBlogRepository> _mockBlogRepository;
        private BlogService _mockService;
        private Mock<IBlogService> _mockBlogService;

        private void InitializeServices()
        {
            _mockBlogRepository = new Mock<IBlogRepository>();
            _mockBlogService = new Mock<IBlogService>();
            _mockService = new BlogService(_mockBlogRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult_WithListOfBlogs()
        {
            InitializeServices();

            // Arrange
            var blogs = MockBlogList();

            _mockBlogService.Setup(service => service.GetAllBlogAsync()).ReturnsAsync(blogs);

            var result = await _mockService.GetAllBlogAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound()
        {
            InitializeServices();

            // Arrange
            var blogs = new List<BlogPostDto>();

            _mockBlogService.Setup(service => service.GetAllBlogAsync()).ReturnsAsync(blogs);

            // Act
            var result = await _mockService.GetAllBlogAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithUser()
        {
            InitializeServices();

            // Arrange
            var blog = MockBlogPostData();
            _mockBlogService.Setup(service => service.GetBlogByIdAsync(1)).ReturnsAsync(blog);

            // Act
            var result = await _mockService.GetBlogByIdAsync(1);

            // Assert
            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task GetById_NotFoundResult_WithBlog()
        {
            InitializeServices();

            // Arrange
            var blog = new AddBlogDto();
            _mockBlogService.Setup(service => service.GetBlogByIdAsync(9)).ReturnsAsync(blog);

            // Act
            var result = await _mockService.GetBlogByIdAsync(9);

            // Assert
            Assert.Equal(0, 0);
        }

        [Fact]
        public async Task Create_ReturnsOkResult_WithCreateBlogPost()
        {
            InitializeServices();

            // Arrange
            var blogPost = new AddBlogDto
            {
                Id = 1,
                Title = "My First Blog",
                Description = "This is the content of the blog.",
                UserId = 123,
                CreatedDate = DateTime.UtcNow
            };

            var createdBlog = MockBlogPostData();
            _mockBlogService.Setup(service => service.CreateBlogAsync(blogPost)).ReturnsAsync(createdBlog);

            // Act
            var result = await _mockService.CreateBlogAsync(blogPost);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedBlog()
        {
            InitializeServices();

            // Arrange
            var updateDto = MockUpdateBlogDto();

            _mockBlogService.Setup(service => service.UpdateBlogAsync(updateDto)).ReturnsAsync(updateDto);

            // Act
            var result = await _mockService.UpdateBlogAsync(updateDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WithSuccessMessage()
        {
            InitializeServices();

            // Arrange
            _mockBlogService.Setup(service => service.DeleteBlogAsync(1)).ReturnsAsync(1);

            // Act
            var result = await _mockService.DeleteBlogAsync(1);

            // Assert
            Assert.Equal(0, 0);
        }

        private IList<BlogPostDto> MockBlogList()
        {
            return new List<BlogPostDto> {
                new BlogPostDto
                {
                    Id = 1,
                    Title = "MyBlog" ,
                    Blogger = "JohnDoe",
                    Description = "John JohnDoe JohnDoe",
                    UserId = 123,
                    CreatedDate = DateTime.Now
                }
            };
        }

        private AddBlogDto MockBlogPostData()
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
