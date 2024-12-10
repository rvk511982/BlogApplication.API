namespace BlogApp.API.Models.DTOs.BlogPost
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// DTO class for updating blog
    /// </summary>
    public class UpdateBlogDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime UpdatedDate { get; set; }
    }
}
