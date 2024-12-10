using BlogApp.API.Models.DTOs.BlogPost;

namespace BlogApp.API.Services.Interface
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Service interface for providing methods to service client
    /// </summary>
    public interface IBlogService
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Blog data as list
        /// </summary>
        /// <returns>Returns lists of Blogs</returns>
        Task<IEnumerable<BlogPostDto>> GetAllBlogAsync();

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing Blog data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns Blog Data</returns>
        Task<AddBlogDto> GetBlogByIdAsync(int id);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new blog record
        /// </summary>
        /// <param name="blog">Blog model object</param>
        /// <returns>Returns newly added blog data</returns>
        Task<AddBlogDto> CreateBlogAsync(AddBlogDto blog);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing blog data
        /// </summary>
        /// <param name="blog">Blog model object</param>
        /// <returns>Returns updated blog data</returns>
        Task<UpdateBlogDto> UpdateBlogAsync(UpdateBlogDto blog);

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        Task<int> DeleteBlogAsync(int id);
    }
}
