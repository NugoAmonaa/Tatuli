using Final.Dto;
using Final.Entities;

namespace Final.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();

        public Task AddUser(AddUserDto user);

        public Task DeleteUser(int id);

        public Task UpdateUser(UpdateUserDto user);

        public Task<User> GetUser(string email);
    }
}
