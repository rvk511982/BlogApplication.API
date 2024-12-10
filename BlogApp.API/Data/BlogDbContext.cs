using BlogApp.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }

        public DbSet<Blog>? Blogs { get; set; }  

        public DbSet<BlogComment>? BlogComments { get; set; }
    }
}
