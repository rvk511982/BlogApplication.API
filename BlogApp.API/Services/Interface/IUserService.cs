using BlogApp.API.Models.DTOs.User;

namespace BlogApp.API.Services.Interface
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Service interface for providing methods to service client
    /// </summary>
    public interface IUserService
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get User data as list
        /// </summary>
        /// <returns>Returns lists of users</returns>
        Task<IEnumerable<UsersDto>> GetAllAsync();

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing user data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns user data</returns>
        Task<AddUserDto> GetUserByIdAsync(int id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new user record
        /// </summary>
        /// <param name="user">AddUserDto requestDto</param>
        /// <returns>Returns newly added user data</returns>
        Task<AddUserDto> CreateUserAsync(AddUserDto user);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing user data
        /// </summary>
        /// <param name="user">UpdateUserDto requestDto</param>
        /// <returns>Returns updated user data</returns>
        Task<UpdateUserDto> UpdateUserAsync(UpdateUserDto user);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        Task<int> DeleteUserAsync(int id);
    }
}
