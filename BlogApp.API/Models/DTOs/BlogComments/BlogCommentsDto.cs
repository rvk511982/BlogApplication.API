namespace BlogApp.API.Models.DTOs.BlogComments
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// DTO class for blog comment data
    /// </summary>
    public class BlogCommentsDto
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int BlogId { get; set; }
    }
}
