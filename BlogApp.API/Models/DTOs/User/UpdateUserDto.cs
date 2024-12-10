namespace BlogApp.API.Models.DTOs.User
{
    /// <summary>
    /// DTO class for updating user data
    /// </summary>
    public class UpdateUserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string HashedPassword { get; set; } = string.Empty;

        public DateTime UpdatedDate { get; set; }
    }
}
