﻿
using Final.database;
using Final.Entities;
using Final.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _context;
        public PostRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddPost(Post Post)
        {
            await _context.Posts.AddAsync(Post);
            await _context.SaveChangesAsync();

        }

        public async Task DeletePost(int id)
        {
            var Post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(Post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetSinglePost(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _context.Posts.ToListAsync();


        }

        public async Task UpdatePost(Post Post)
        {

            _context.Posts.Update(Post);
            await _context.SaveChangesAsync();
        }
    }
}