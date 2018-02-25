using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Controllers;
using blog.Database;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace blog.tests {
    [Collection("Database")]
    public class PostControllerTest : IClassFixture<DatabaseFixture> {
        DatabaseFixture _fixture;
        public PostControllerTest(DatabaseFixture fixture) {
            _fixture = fixture;

            _fixture.AddSampleData();
        }

        [Fact]
        public void GetAll() {
            using(var context = _fixture.CreateContext()) {
                var controller = new PostController(context);
                var posts = controller.Get();
                Assert.True(posts.Count()== _fixture.SampleCount);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Get() {
            using(var context = _fixture.CreateContext()) {
                var controller = new PostController(context);
                Post post = (await controller.Get(_fixture.SampleGuid)as JsonResult).Value as Post;
                Assert.NotNull(post);
                Assert.Equal(_fixture.SampleGuid, post.Id);

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Create() {
            using(var context = _fixture.CreateContext()) {
                var controller = new PostController(context);
                var postCountBefore = context.Posts.Count();
                var newPost = _fixture.NewPost.Generate();
                var resultPost = await controller.Create(newPost);

                Assert.True(resultPost.Id != Guid.Empty && resultPost.Id != null);
                Assert.Equal(postCountBefore + 1, context.Posts.Count());

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public async Task Update() {
            using(var context = _fixture.CreateContext()) {
                var controller = new PostController(context);
                var post = context.Posts.First();
                post.Title = "New Title";
                Post resultPost = (await controller.Update(post.Id, post)as JsonResult).Value as Post;
                var dbResult = context.Posts.Find(resultPost.Id);
                Assert.True(resultPost.Title == "New Title");

                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void Latest() {
            using(var context = _fixture.CreateContext()) {
                var controller = new PostController(context);
                var latestPosts = controller.Latest(3);

                Assert.Equal(3, latestPosts.Count());

                context.Database.EnsureDeleted();
            }
        }
    }
}