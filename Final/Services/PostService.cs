using Final.Dto;
using Final.Entities;
using Final.Interfaces;
using Final.IRepositories;
using Final.Repositories;

namespace Final.Services
{
    public class PostService : IPostService
    {
        public IPostRepository PostRepository;
        public IUserRepository UserRepository;
        public ICommentRepository CommentRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            UserRepository = userRepository;
            PostRepository = postRepository;
            CommentRepository = commentRepository;
        }

        public async Task AddComment(AddCommentDto commentDto)
        {
            try
            {
                var user = await UserRepository.GetSingleUser(commentDto.UserId);
                var post = await PostRepository.GetSinglePost(commentDto.PostID);
                var comment = new Comment
                {

                    Content = commentDto.Content,
                    User = user,
                    Post = post,
                    UserId = commentDto.UserId,
                    PostID = commentDto.PostID

                };
                await CommentRepository.AddComment(comment);
            }
            catch (Exception ex) 
            { var a = 5; }
        }

        public async Task AddPost(AddPostDto postDto)
        {
            var user = await UserRepository.GetSingleUser(postDto.CreatorId);
            var post = new Post
            {
                Name = postDto.Name,
                Content = postDto.Content,
                Creator = user

            };
            await PostRepository.AddPost(post);

        }

        public async Task DeletePost(int id)
        {
            await PostRepository.DeletePost(id);
        }

        public async Task<List<Post>> GetPosts()
        {
           return await PostRepository.GetPosts();
        }

        public async Task UpdatePost(UpdatePostDto postDto)
        {
            var post = new Post
            {
                Id = postDto.Id,
                Name = postDto.Name,
                Content = postDto.Content,
                CreatorId = postDto.CreatorId

            };
            await PostRepository.UpdatePost(post);
        }
    }
}
