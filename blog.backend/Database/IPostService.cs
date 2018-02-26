using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using blog.Models;

namespace blog.backend.Database {
    public interface IPostService {
        Task<Post> CreateAsync(Post post);
        Task<Post> GetByIdAsync(Guid id);
        Task<List<Post>> GetAllAsync();
        Task<List<Post>> GetLatestAsync(int count);
        Task<Post> UpdateAsync(Post post);
        Task<Boolean> DeleteAsync(Guid id);
    }
}