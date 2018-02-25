using System;
using System.Collections.Generic;
using blog.Database;
using blog.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace blog.tests {
    public class DatabaseFixture : IDisposable {
        private DbContextOptions<BlogContext> _options;
        public DatabaseFixture() {
            var builder = new DbContextOptionsBuilder<BlogContext>();
            builder.UseInMemoryDatabase("testDB");
            _options = builder.Options;

            Randomizer.Seed = new Random(123456);
        }

        public void AddSampleData() {
            using(var context = CreateContext()) {
                var posts = NewPost
                    .Generate(SampleCount);
                posts[0].Id = SampleGuid;

                context.Posts.AddRange(posts);
                context.SaveChanges();
            }
        }

        public void Dispose() {
            using(var context = CreateContext()) {
                context.Database.EnsureDeleted();
            }
        }

        public BlogContext CreateContext() {
            return new BlogContext(_options);
        }

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