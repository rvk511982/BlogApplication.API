using BlogApp.API.Models.DTOs.BlogPost;
using BlogApp.API.Models.Entities;
using BlogApp.API.Repositories.Interface;
using BlogApp.API.Services.Interface;

namespace BlogApp.API.Services
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Service class for handling blog crud operations
    /// </summary>
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="blogRepository">IUserRepository blogRepository</param>
        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new blog record
        /// </summary>
        /// <param name="requestDto">AddBlogDto requestDto</param>
        /// <returns>Returns newly added blog data</returns>
        public async Task<AddBlogDto> CreateBlogAsync(AddBlogDto requestDto)
        {
            var newBlog = new Blog
            {
                Description = requestDto.Description,
                Title = requestDto.Title,
                UserId = requestDto.UserId,
                CreatedDate = requestDto.CreatedDate,
                IsDeleted = false
            };

            var blogData = await _blogRepository.CreateAsync(newBlog);
            if (blogData != null)
            {
                return await MapBlogPostData(blogData.Id);
            }
            else
            {
                return new AddBlogDto();
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        public async Task<int> DeleteBlogAsync(int id)
        {
            return await _blogRepository.DeleteAsync(id);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Getting all valid blog data
        /// </summary>
        /// <returns>Returns lists of blog</returns>
        public async Task<IEnumerable<BlogPostDto>> GetAllBlogAsync()
        {
            var response = new List<BlogPostDto>();
            var result = await _blogRepository.GetAllAsync();
            if (result != null)
            {
                foreach (var item in result)
                {
                    var data = new BlogPostDto();

                    data.Id = item.Id;
                    data.UserId = item.UserId;
                    data.Title = item.Title;
                    data.Description = item.Description;
                    data.Blogger = item.User.Username;
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
        public async Task<AddBlogDto> GetBlogByIdAsync(int id)
        {
            return await MapBlogPostData(id);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing blog record
        /// </summary>
        /// <param name="requestDto">UpdateBlogDto requestDto</param>
        /// <returns>Returns updated blog data</returns>
        public async Task<UpdateBlogDto> UpdateBlogAsync(UpdateBlogDto requestDto)
        {
            var data = new UpdateBlogDto();
            var blog = new Blog
            {
                Id = requestDto.Id,
                UserId = requestDto.UserId,
                Title = requestDto.Title,
                Description = requestDto.Description,
                IsDeleted = false,
                UpdatedDate = requestDto.UpdatedDate
            };

            var result = await _blogRepository.UpdateAsync(blog);
            if (result != null)
            {
                data.Id = result.Id;
                data.UserId = result.UserId;
                data.Title = result.Title;
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
        private async Task<AddBlogDto> MapBlogPostData(int id)
        {
            var data = new AddBlogDto();
            var result = await _blogRepository.GetByIdAsync(id);
            if (result != null && result.Id > 0)
            {
                data.Id = result.Id;
                data.UserId = result.UserId;
                data.Title = result.Title;
                data.Description = result.Description;
                data.CreatedDate = result.CreatedDate;
            }
            return data;
        }
    }
}
