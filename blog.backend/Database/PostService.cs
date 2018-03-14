using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Database;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.backend.Database {
    public class PostService : IPostService {
        DatabaseContext _context;

        public PostService(DatabaseContext context) {
            _context = context;
        }
        public async Task<Post> CreateAsync(Post post) {
            post.Created = DateTime.UtcNow;
            post.Modified = DateTime.UtcNow;
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> UpdateAsync(Post post) {
            post.Modified = DateTime.UtcNow;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> GetByIdAsync(Guid id) {
            return await _context.Posts.FindAsync(id);
        }

        public Task<List<Post>> GetAllAsync() {
            return _context.Posts.ToListAsync();
        }

        public Task<List<Post>> GetLatestAsync(int count) {
            return _context.Posts.OrderBy(post => post.Created).Take(count).ToListAsync();
        }

        public async Task<Boolean> DeleteAsync(Guid id){
            var post = await _context.Posts.FindAsync(id);
            if(post == null){
                return false;
            } else {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}