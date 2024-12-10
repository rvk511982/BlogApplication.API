using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Models.Entities
{
    /// <summary>
    /// Db Entity class for Blog
    /// </summary>
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public User? User { get; set; }
    }
}
