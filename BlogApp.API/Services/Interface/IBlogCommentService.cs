using BlogApp.API.Models.DTOs.BlogComments;

namespace BlogApp.API.Services.Interface
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Service interface for providing methods to service client
    /// </summary>
    public interface IBlogCommentService
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Blog comments as list
        /// </summary>
        /// <returns>Returns lists of blog comments</returns>
        Task<IEnumerable<BlogCommentsDto>> GetAllBlogCommentAsync();

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing blog comment by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns blog comment</returns>
        Task<AddBlogCommentDto> GetBlogCommentByIdAsync(int id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new blog comment record
        /// </summary>
        /// <param name="blogComment">BlogComment model object</param>
        /// <returns>Returns newly added blog comment</returns>
        Task<AddBlogCommentDto> CreateBlogCommentAsync(AddBlogCommentDto blogComment);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing blog comment
        /// </summary>
        /// <param name="blogComment">BlogComment model object</param>
        /// <returns>Returns updated blog comment</returns>
        Task<UpdateBlogCommentDto> UpdateBlogCommentAsync(UpdateBlogCommentDto blogComment);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing blog comments by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        Task<int> DeleteBlogCommentAsync(int id);
    }
}
