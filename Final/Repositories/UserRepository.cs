
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
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
           await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetSingleUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();


        }

        public async Task UpdateUser(User user)
        {
           
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}