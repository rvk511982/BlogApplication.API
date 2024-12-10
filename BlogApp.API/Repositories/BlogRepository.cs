using BlogApp.API.Data;
using BlogApp.API.Models.Entities;
using BlogApp.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Repositories
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Repository class for implementing CRUD operation with DB for Blog.
    /// </summary>
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _dbContext;

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        public BlogRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Getting all blog data
        /// </summary>
        /// <returns>Returns lists of blogs</returns>
        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _dbContext.Blogs
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(o => o.Id).ToListAsync();
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new blog record
        /// </summary>
        /// <param name="blog">Blog model object</param>
        /// <returns>Returns newly added blog data</returns>
        public async Task<Blog> CreateAsync(Blog blog)
        {
            await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();
            return blog;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing blog data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns blog data</returns>
        public async Task<Blog> GetByIdAsync(int id)
        {
            return await GetBlogById(id);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(int id)
        {
            var data = await GetBlogById(id);
            if (data != null)
            {
                data.IsDeleted = true;
            }
            return await _dbContext.SaveChangesAsync();
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing blog record
        /// </summary>
        /// <param name="blog">Blog model object</param>
        /// <returns>Returns updated blog data</returns>
        public async Task<Blog> UpdateAsync(Blog blog)
        {
            _dbContext.Update(blog);
            await _dbContext.SaveChangesAsync();
            return blog;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get the blog data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns blog data</returns>
        private async Task<Blog> GetBlogById(int id)
        {
            return await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }
    }
}
