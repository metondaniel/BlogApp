using BlogApp.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repository
{
    public class BlogContext : IdentityDbContext<User>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) 
        { 
        }

        public DbSet<Post> Posts { get; set; }
    }
}
