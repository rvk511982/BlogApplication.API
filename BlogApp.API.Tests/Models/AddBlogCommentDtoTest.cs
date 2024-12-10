using BlogApp.API.Models.DTOs.BlogComments;
using FluentAssertions;

namespace BlogApp.API.Tests.Models
{
    public class AddBlogCommentDtoTest
    {
        [Fact]
        public void BlogComment_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var blogPost = new AddBlogCommentDto
            {
                Id = 1,
                Description = "This is the content of the blog.",
                BlogId = 12,
                CreatedDate = DateTime.UtcNow
            };

            // Assert
            blogPost.Id.Should().Be(1);
            blogPost.Description.Should().Be("This is the content of the blog.");
            blogPost.BlogId.Should().Be(12);
            blogPost.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1)); // Allow small time variance
        }

        [Fact]
        public void BlogComment_ShouldHaveDefaultValues()
        {
            // Arrange & Act
            var blogPost = new AddBlogCommentDto();
            blogPost.CreatedDate = DateTime.UtcNow;
            blogPost.BlogId = 0;

            // Assert
            blogPost.Id.Should().Be(0); // Default int value
            blogPost.Description.Should().BeNull();
            blogPost.BlogId.Should().Be(0);
            blogPost.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1)); // Default UTC now
        }

        [Fact]
        public void BlogComment_ShouldAllowNullTitle()
        {
            // Arrange & Act
            var blogPost = new AddBlogCommentDto { Description = null };

            // Assert
            blogPost.Description.Should().BeNull();
        }

        [Fact]
        public void BlogComment_ShouldAllowEmptyContent()
        {
            // Arrange & Act
            var blogPost = new AddBlogCommentDto { Description = "" };

            // Assert
            blogPost.Description.Should().BeEmpty();
        }

        [Fact]
        public void BlogComment_ShouldAcceptCustomCreatedAt()
        {
            // Arrange
            var customDate = new DateTime(2000, 1, 1);

            // Act
            var blogPost = new AddBlogCommentDto { CreatedDate = customDate };

            // Assert
            blogPost.CreatedDate.Should().Be(customDate);
        }
    }
}
