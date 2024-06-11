using Final.Dto;
using Final.Entities;

namespace Final.Interfaces
{
    public interface IPostService
    {
        public Task<List<Post>> GetPosts();

        public Task AddPost(AddPostDto post);
        public Task AddComment(AddCommentDto comment);

        public Task DeletePost(int id);

        public Task UpdatePost(UpdatePostDto Post);

       
    }
}
