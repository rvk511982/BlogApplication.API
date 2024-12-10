using BlogApp.API.Models.Entities;

namespace BlogApp.API.Repositories.Interface
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Repository interface for providing methods to repository client
    /// </summary>
    public interface IUserRepository
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get User data as list
        /// </summary>
        /// <returns>Returns lists of users</returns>
        Task<IEnumerable<User>> GetAllAsync();

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing user data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns user data</returns>
        Task<User> GetByIdAsync(int id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new user record
        /// </summary>
        /// <param name="user">User requestDto</param>
        /// <returns>Returns newly added user data</returns>
        Task<User> CreateAsync(User user);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing user data
        /// </summary>
        /// <param name="user">User requestDto</param>
        /// <returns>Returns updated user data</returns>
        Task<User> UpdateAsync(User user);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        Task<int> DeleteAsync(int id);
    }
}
