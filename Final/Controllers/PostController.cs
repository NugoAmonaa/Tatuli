using Final.Dto;
using Final.Entities;
using Final.Interfaces;
using Final.IRepositories;
using Final.Services;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [ApiController]
    [Route("Post")]
    public class PostController : ControllerBase
    {
        private IPostService _PostService;


        public PostController(IPostService PostService)
        {
            _PostService = PostService;
        }
        [HttpGet]
        public async Task<List<Post>> GetPosts()
        {
            return await _PostService.GetPosts();

        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostDto addPostDto)
        {
            await _PostService.AddPost(addPostDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _PostService.DeletePost(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(UpdatePostDto updatePostDto)
        {
            await _PostService.UpdatePost(updatePostDto);
            return Ok();
        }

        [HttpPost( "AddComment")]
        public async Task<IActionResult> AddComment(AddCommentDto addCommentDto)
        {
            await _PostService.AddComment(addCommentDto);
            return Ok();
        }


    }

}

