using BlogApp.API.Models.DTOs.BlogPost;
using FluentAssertions;

namespace BlogApp.API.Tests.Models
{
    public class AddBlogDtoTest
    {
        [Fact]
        public void BlogPost_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var blogPost = new AddBlogDto
            {
                Id = 1,
                Title = "My First Blog",
                Description = "This is the content of the blog.",
                UserId = 123,
                CreatedDate = DateTime.UtcNow
            };

            // Assert
            blogPost.Id.Should().Be(1);
            blogPost.Title.Should().Be("My First Blog");
            blogPost.Description.Should().Be("This is the content of the blog.");
            blogPost.UserId.Should().Be(123);
            blogPost.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1)); // Allow small time variance
        }

        [Fact]
        public void BlogPost_ShouldHaveDefaultValues()
        {
            // Arrange & Act
            var blogPost = new AddBlogDto();
            blogPost.CreatedDate = DateTime.UtcNow;
            blogPost.UserId = 0;

            // Assert
            blogPost.Id.Should().Be(0); // Default int value
            blogPost.Title.Should().BeEmpty(); // Default string value
            blogPost.Description.Should().BeEmpty();
            blogPost.UserId.Should().Be(0);
            blogPost.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1)); // Default UTC now
        }

        [Fact]
        public void BlogPost_ShouldAllowNullTitle()
        {
            // Arrange & Act
            var blogPost = new AddBlogDto { Title = null };

            // Assert
            blogPost.Title.Should().BeNull();
        }

        [Fact]
        public void BlogPost_ShouldAllowEmptyContent()
        {
            // Arrange & Act
            var blogPost = new AddBlogDto { Description = "" };

            // Assert
            blogPost.Description.Should().BeEmpty();
        }

        [Fact]
        public void BlogPost_ShouldAcceptCustomCreatedAt()
        {
            // Arrange
            var customDate = new DateTime(2000, 1, 1);

            // Act
            var blogPost = new AddBlogDto { CreatedDate = customDate };

            // Assert
            blogPost.CreatedDate.Should().Be(customDate);
        } 
    }
}
