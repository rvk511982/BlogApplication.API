using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Models.Entities
{
    /// <summary>
    /// Db Entity class for Blog Comments
    /// </summary>
    public class BlogComment
    {
        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int BlogId { get; set; }

        public Blog? Blog { get; set; }
    }
}
