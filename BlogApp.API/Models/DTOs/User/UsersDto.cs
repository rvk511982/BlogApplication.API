namespace BlogApp.API.Models.DTOs.User
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// DTO class for user data
    /// </summary>
    public class UsersDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
