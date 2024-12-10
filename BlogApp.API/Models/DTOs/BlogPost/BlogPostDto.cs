namespace BlogApp.API.Models.DTOs.BlogPost
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// DTO class for blogs
    /// </summary>
    public class BlogPostDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Blogger { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
