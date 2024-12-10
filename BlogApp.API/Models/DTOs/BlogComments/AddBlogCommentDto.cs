namespace BlogApp.API.Models.DTOs.BlogComments
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// DTO class for adding new blog comment data
    /// </summary>
    public class AddBlogCommentDto
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
