using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Database;
using blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers {
    [Route("api/[controller]")]
    public class PostController : Controller {
        private readonly BlogContext _context;
        public PostController(BlogContext context) {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Post> Get() {
            return _context.Posts.ToList();
        }

        [HttpGet("latest/{count:int}")]
        public IEnumerable<Post> Latest(int count) {
            return _context.Posts.OrderBy(post => post.Created).Take(count);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) {
            var article = await _context.Posts.FindAsync(id);
            if (article == null) {
                return NotFound();
            }
            return Json(article);
        }

        // POST api/values
        [HttpPost]
        public async Task<Post> Post([FromBody] Post value) {
            value.Created = DateTime.UtcNow;
            value.Modified = DateTime.UtcNow;
            await _context.Posts.AddAsync(value);

            await _context.SaveChangesAsync();

            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Post value) {
            if (value.Id == null || value.Id == Guid.Empty) {
                return NotFound();
            }
            value.Modified = DateTime.UtcNow;
            _context.Posts.Update(value);
            await _context.SaveChangesAsync();
            return Json(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) {
                return Json(new NotFoundObjectResult(id));
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}