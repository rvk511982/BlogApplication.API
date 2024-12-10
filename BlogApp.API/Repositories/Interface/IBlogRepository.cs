using BlogApp.API.Models.Entities;

namespace BlogApp.API.Repositories.Interface
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Repository interface for providing methods to repository client
    /// </summary>
    public interface IBlogRepository
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Blog data as list
        /// </summary>
        /// <returns>Returns lists of Blogs</returns>
        Task<IEnumerable<Blog>> GetAllAsync();

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing Blog data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns Blog Data</returns>
        Task<Blog> GetByIdAsync(int id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new blog record
        /// </summary>
        /// <param name="blog">Blog model object</param>
        /// <returns>Returns newly added blog data</returns>
        Task<Blog> CreateAsync(Blog blog);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing blog data
        /// </summary>
        /// <param name="blog">Blog model object</param>
        /// <returns>Returns updated blog data</returns>
        Task<Blog> UpdateAsync(Blog blog);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        Task<int> DeleteAsync(int id);
    }
}
