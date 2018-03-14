using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Database {
    public class DatabaseContext : DbContext {

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Post>().Property<string>("TagCollection").HasField("_tags");
        }
    }
}