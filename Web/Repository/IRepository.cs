using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        IList<Post> GetAllPosts();
        void RemovePost(int id);
        void UpdatePost(Post item);
        void AddPost(Post item);
        Task<bool> SaveChangesAsync();
    }
}