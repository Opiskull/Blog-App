using System;
using System.Collections.Generic;
using blog.backend.Database;
using blog.Database;
using blog.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace blog.tests {
    public class DatabaseFixture {

        public DatabaseFixture() {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<BlogContext>(options => options.UseInMemoryDatabase("testDB"), ServiceLifetime.Transient);
            serviceCollection.AddTransient<IPostService, PostService>();
            ServiceProvider = serviceCollection.BuildServiceProvider();

            Randomizer.Seed = new Random(123456);
        }

        public void AddSampleData() {
            var context = ServiceProvider.GetService<BlogContext>();
            var posts = NewPost
                .Generate(SampleCount);
            posts[0].Id = SampleGuid;

            context.Posts.AddRange(posts);
            context.SaveChanges();
        }

        public BlogContext CreateContext() {
            return ServiceProvider.GetService<BlogContext>();
        }

        public T GetService<T>() {
            return ServiceProvider.GetService<T>();
        }

        public IServiceProvider ServiceProvider { get; set; }

        public Faker<Post> NewPost = new Faker<Post>()
            .RuleFor(p => p.Content, f => f.Lorem.Lines(f.Random.Number(1, 15)))
            .RuleFor(p => p.Created, f => f.Date.Recent())
            .RuleFor(p => p.Title, f => f.Lorem.Sentence())
            .RuleFor(p => p.Modified, f => f.Date.Recent())
            .RuleFor(p => p.Tags, f => f.Commerce.Categories(f.Random.Number(1, 5)));

        public Guid SampleGuid = Guid.Parse("360f830e-d895-4d9d-a3e7-7c586f5423da");
        public int SampleCount = 5;
    }
}