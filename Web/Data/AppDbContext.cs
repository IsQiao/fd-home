using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
        {
            
        }
        
        public DbSet<Post> Posts { get; set; }
    }
}