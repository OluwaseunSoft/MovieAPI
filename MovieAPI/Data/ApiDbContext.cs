using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Model;

namespace MovieAPI.Data
{
    public class ApiDbContext : IdentityDbContext<IdentityUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
            { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
