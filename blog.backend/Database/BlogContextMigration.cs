using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace blog.Database {
    public static class BlogContextMigration {
        public static void MigrateBlogContext(this IServiceProvider serviceProvider) {
            using(var scope = serviceProvider.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
                    services.GetService<BlogContext>().Database.Migrate();
                } catch (Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Could not Migrate Database");
                }
            }
        }
    }
}