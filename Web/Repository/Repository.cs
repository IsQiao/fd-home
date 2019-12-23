using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Post GetPost(int id)
        {
            return _dbContext.Posts.FirstOrDefault(x => x.Id == id);
        }

        public IList<Post> GetAllPosts()
        {
            return _dbContext.Posts.ToList();
        }

        public void RemovePost(int id)
        {
            var item = _dbContext.Posts.FirstOrDefault(x => x.Id == id);
            if (item != null)
                _dbContext.Posts.Remove(item);
        }

        public void UpdatePost(Post item)
        {
            _dbContext.Update(item);
        }

        public void AddPost(Post item)
        {
            _dbContext.Add(item);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}