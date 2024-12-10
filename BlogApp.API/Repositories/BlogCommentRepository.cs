using BlogApp.API.Data;
using BlogApp.API.Models.Entities;
using BlogApp.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Repositories
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Repository class for implementing CRUD operation with DB for Blog Commnet.
    /// </summary>
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly BlogDbContext _dbContext;

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        public BlogCommentRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Getting all Blog Commnets
        /// </summary>
        /// <returns>Returns lists of Blog Commnets</returns>
        public async Task<IEnumerable<BlogComment>> GetAllAsync()
        {
            return await _dbContext.BlogComments
                .Where(x => x.Blog.IsDeleted == false)
                .OrderByDescending(o => o.Id).ToListAsync();
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new Blog Commnet record
        /// </summary>
        /// <param name="blog">Blog model object</param>
        /// <returns>Returns newly added Blog Commnet data</returns>
        public async Task<BlogComment> CreateAsync(BlogComment blog)
        {
            await _dbContext.BlogComments.AddAsync(blog);
            await _dbContext.SaveChangesAsync();
            return blog;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing Blog Commnet data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns Blog Commnet data</returns>
        public async Task<BlogComment> GetByIdAsync(int id)
        {
            return await GetBlogById(id);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing Blog Commnet by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(int id)
        {
            var data = await GetBlogById(id);
            if (data != null)
            {
                _dbContext.BlogComments.Remove(data);
            }
            return await _dbContext.SaveChangesAsync();
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing Blog Commnet record
        /// </summary>
        /// <param name="blog">Blog model object</param>
        /// <returns>Returns updated Blog Commnet data</returns>
        public async Task<BlogComment> UpdateAsync(BlogComment blog)
        {
            _dbContext.Update(blog);
            await _dbContext.SaveChangesAsync();
            return blog;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get the Blog Commnet data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns Blog Commnet data</returns>
        private async Task<BlogComment> GetBlogById(int id)
        {
            return await _dbContext.BlogComments.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
