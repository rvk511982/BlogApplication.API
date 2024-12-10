namespace BlogApp.API.Models.DTOs.BlogComments
{
    /// <summary>
    /// DTO class for updating blog comment data
    /// </summary>
    public class UpdateBlogCommentDto
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public string? Description { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
