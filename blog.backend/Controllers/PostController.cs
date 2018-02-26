using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.backend.Database;
using blog.Database;
using blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers {
    [Route("api/[controller]")]
    public class PostController : Controller {
        private readonly IPostService _postService;
        public PostController(IPostService postService) {
            _postService = postService;
        }

        [HttpGet]
        public Task<List<Post>> Get() {
            return _postService.GetAllAsync();
        }

        [HttpGet("latest/{count:int}")]
        public Task<List<Post>> Latest(int count) {
            return _postService.GetLatestAsync(count);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) {
            var article = await _postService.GetByIdAsync(id);
            if (article == null) {
                return NotFound();
            }
            return Json(article);
        }

        [HttpPost]
        public Task<Post> Create([FromBody] Post post) {
            return _postService.CreateAsync(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Post post) {
            if (post.Id == null || post.Id == Guid.Empty) {
                return NotFound();
            }
            await _postService.UpdateAsync(post);
            return Json(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var result = await _postService.DeleteAsync(id);
            if (result) {
                return NoContent();
            } else {
                return Json(new NotFoundObjectResult(id));
            }
        }
    }
}