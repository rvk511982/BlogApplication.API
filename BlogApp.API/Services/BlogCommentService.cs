using BlogApp.API.Models.DTOs.BlogComments;
using BlogApp.API.Models.Entities;
using BlogApp.API.Repositories.Interface;
using BlogApp.API.Services.Interface;

namespace BlogApp.API.Services
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Service class for handling blog comment crud operations
    /// </summary>
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IBlogCommentRepository _blogCommentRepository;

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="blogCommentRepository">IBlogCommentRepository blogCommentRepository</param>
        public BlogCommentService(IBlogCommentRepository blogCommentRepository)
        {
            _blogCommentRepository = blogCommentRepository;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new blog record
        /// </summary>
        /// <param name="requestDto">AddBlogDto requestDto</param>
        /// <returns>Returns newly added blog data</returns>
        public async Task<AddBlogCommentDto> CreateBlogCommentAsync(AddBlogCommentDto requestDto)
        {
            var newBlog = new BlogComment
            {
                Description = requestDto.Description,
                BlogId = requestDto.BlogId,
                CreatedDate = requestDto.CreatedDate
            };

            var blogData = await _blogCommentRepository.CreateAsync(newBlog);
            if (blogData != null)
            {
                return await MapBlogCommentData(blogData.Id);
            }
            else
            {
                return new AddBlogCommentDto();
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        public async Task<int> DeleteBlogCommentAsync(int id)
        {
            return await _blogCommentRepository.DeleteAsync(id);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Getting all valid blog data
        /// </summary>
        /// <returns>Returns lists of blog</returns>
        public async Task<IEnumerable<BlogCommentsDto>> GetAllBlogCommentAsync()
        {
            var response = new List<BlogCommentsDto>();
            var result = await _blogCommentRepository.GetAllAsync();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var data = new BlogCommentsDto();

                    data.Id = item.Id;
                    data.BlogId = item.BlogId;
                   
                    data.Description = item.Description;
                    
                    data.CreatedDate = item.CreatedDate;

                    if (data != null && data.Id > 0)
                        response.Add(data);
                }
            }
            return response;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing blog data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns blog data</returns>
        public async Task<AddBlogCommentDto> GetBlogCommentByIdAsync(int id)
        {
            return await MapBlogCommentData(id);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing blog record
        /// </summary>
        /// <param name="requestDto">UpdateBlogDto requestDto</param>
        /// <returns>Returns updated blog data</returns>
        public async Task<UpdateBlogCommentDto> UpdateBlogCommentAsync(UpdateBlogCommentDto requestDto)
        {
            var data = new UpdateBlogCommentDto();
            var blog = new BlogComment
            {
                Id = requestDto.Id,
                BlogId = requestDto.BlogId,
                Description = requestDto.Description,
                UpdatedDate = requestDto.UpdatedDate
            };

            var result = await _blogCommentRepository.UpdateAsync(blog);
            if (result != null)
            {
                data.Id = result.Id;
                data.BlogId = result.BlogId;
                data.Description = result.Description;
                data.UpdatedDate = result.UpdatedDate;
            }
            return data;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Mapping blog data from enity model to DTO model
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns blog data</returns>
        private async Task<AddBlogCommentDto> MapBlogCommentData(int id)
        {
            var data = new AddBlogCommentDto();
            var result = await _blogCommentRepository.GetByIdAsync(id);
            if (result != null && result.Id > 0)
            {
                data.Id = result.Id;
                data.BlogId = result.BlogId;
                data.Description = result.Description;
                data.CreatedDate = result.CreatedDate;
            }
            return data;
        }
    }
}
