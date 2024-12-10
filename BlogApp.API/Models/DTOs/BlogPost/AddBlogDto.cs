namespace BlogApp.API.Models.DTOs.BlogPost
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// DTO class for adding blog post
    /// </summary>
    public class AddBlogDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
