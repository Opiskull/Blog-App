using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace blog.Database {
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext> {
        public DatabaseContext CreateDbContext(string[] args) {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}