using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Models.Entities
{
    /// <summary>
    /// Db Entity class for User
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }

        public required string Username { get; set; }

        public required string EmailAddress { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string HashedPassword { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
