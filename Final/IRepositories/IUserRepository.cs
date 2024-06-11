using Final.Entities;

namespace Final.IRepositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsers();
        public Task<User> GetSingleUser(int id);
        public Task AddUser(User user);
        public Task UpdateUser(User user);
        public Task DeleteUser(int id);
    }
}
