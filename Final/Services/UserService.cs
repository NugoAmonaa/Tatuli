using Final.Dto;
using Final.Entities;
using Final.Interfaces;
using Final.IRepositories;

namespace Final.Services
{

    public class UserService : IUserService
    {
        public IUserRepository UserRepository; 
        public UserService(IUserRepository userRepository) {
            UserRepository = userRepository;
        }
        public async Task<List<User>> GetUsers()
        {
            return await UserRepository.GetUsers();
         
        }

        public async Task  AddUser(AddUserDto userDto)
        {
            var user = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role
            };
            
             await UserRepository.AddUser(user);
            
        }

        public async Task DeleteUser(int id)
        {
           await UserRepository.DeleteUser(id);
            
        }

        public async Task UpdateUser(UpdateUserDto updateUser)
        {
            var user = new User()

            {   Id = updateUser.Id,
                FirstName = updateUser.FirstName,
                LastName = updateUser.LastName,
                Email = updateUser.Email,
                Password = updateUser.Password,
                Role = updateUser.Role
            };
            await UserRepository.UpdateUser(user);
             
        }

        public async Task<User> GetUser(string email)
        {
             var user = (await UserRepository.GetUsers()).Where(x=>x.Email == email).SingleOrDefault();
            return user;

        }
    }
}
