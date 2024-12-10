using BlogApp.API.Data;
using BlogApp.API.Models.Entities;
using BlogApp.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Repositories
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Repository class for implementing CRUD operation with DB for User.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext _dbContext;

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        public UserRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Getting all valid users data
        /// </summary>
        /// <returns>Returns lists of users</returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(o => o.Id).ToListAsync();
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new user record
        /// </summary>
        /// <param name="user">User requestDto</param>
        /// <returns>Returns newly added user data</returns>
        public async Task<User> CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing user data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns user data</returns>
        public async Task<User> GetByIdAsync(int id)
        {
            return await GetUserById(id);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(int id)
        {
            var userData = await GetUserById(id);
            if (userData != null)
            {
                userData.IsDeleted = true;
            }
            return await _dbContext.SaveChangesAsync();
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing user record
        /// </summary>
        /// <param name="user">User requestDto</param>
        /// <returns>Returns updated user data</returns>
        public async Task<User> UpdateAsync(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get the user data by userId
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        private async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
    }
}
