using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.backend.Database;
using blog.Controllers;
using blog.Database;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace blog.tests {
    [Collection("Database")]
    public class PostControllerTest {
        DatabaseFixture _fixture;
        public PostControllerTest() {
            _fixture = new DatabaseFixture();
        }

        [Fact]
        public async Task GetAll() {
            _fixture.AddSampleData();

            using(var context = _fixture.CreateContext()) {
                var controller = new PostController(_fixture.GetService<IPostService>());
                var posts = await controller.Get();
                Assert.True(posts.Count()== _fixture.SampleCount);
            }
        }

        [Fact]
        public async Task Get() {
            _fixture.AddSampleData();

            var controller = new PostController(_fixture.GetService<IPostService>());
            Post post = (await controller.Get(_fixture.SampleGuid)as JsonResult).Value as Post;
            Assert.NotNull(post);
            Assert.Equal(_fixture.SampleGuid, post.Id);
        }

        [Fact]
        public async Task Create() {
            _fixture.AddSampleData();

            using(var context = _fixture.CreateContext()) {
                var controller = new PostController(_fixture.GetService<IPostService>());
                var postCountBefore = context.Posts.Count();
                var newPost = _fixture.NewPost.Generate();
                var resultPost = await controller.Create(newPost);

                Assert.True(resultPost.Id != Guid.Empty && resultPost.Id != null);
                Assert.Equal(postCountBefore + 1, context.Posts.Count());
            }
        }

        [Fact]
        public async Task Update() {
            _fixture.AddSampleData();

            using(var context = _fixture.CreateContext()) {
                var controller = new PostController(_fixture.GetService<IPostService>());
                var post = context.Posts.First();
                post.Title = "New Title";
                Post resultPost = (await controller.Update(post.Id, post)as JsonResult).Value as Post;
                var dbResult = context.Posts.Find(resultPost.Id);
                Assert.True(resultPost.Title == "New Title");
            }
        }

        [Fact]
        public async Task Latest() {
            _fixture.AddSampleData();

            using(var context = _fixture.CreateContext()) {
                var controller = new PostController(_fixture.GetService<IPostService>());
                var latestPosts = await controller.Latest(3);

                Assert.Equal(3, latestPosts.Count());
            }
        }
    }
}