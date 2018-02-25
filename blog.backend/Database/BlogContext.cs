using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Database {
    public class BlogContext : DbContext {

        public BlogContext(DbContextOptions<BlogContext> options): base(options) { }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Post>().Property<string>("TagCollection").HasField("_tags");
        }
    }
}