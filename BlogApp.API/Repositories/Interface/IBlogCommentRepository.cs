using BlogApp.API.Models.Entities;

namespace BlogApp.API.Repositories.Interface
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Repository interface for providing methods to repository client
    /// </summary>
    public interface IBlogCommentRepository
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Blog comments as list
        /// </summary>
        /// <returns>Returns lists of blog comments</returns>
        Task<IEnumerable<BlogComment>> GetAllAsync();

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing blog comment by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns blog comment</returns>
        Task<BlogComment> GetByIdAsync(int id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new blog comment record
        /// </summary>
        /// <param name="blogComment">BlogComment model object</param>
        /// <returns>Returns newly added blog comment</returns>
        Task<BlogComment> CreateAsync(BlogComment blogComment);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing blog comment
        /// </summary>
        /// <param name="blogComment">BlogComment model object</param>
        /// <returns>Returns updated blog comment</returns>
        Task<BlogComment> UpdateAsync(BlogComment blogComment);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing blog comments by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        Task<int> DeleteAsync(int id);
    }
}
