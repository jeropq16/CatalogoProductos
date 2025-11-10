using catalogo.Domain.Models;
using catalogo.Domain.Interfaces;

namespace catalogo.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User?> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User?> Update(User user)
        {
            var existe = await _userRepository.GetById(user.Id);
            if (existe == null) return null;

            existe.Username = user.Username;
            existe.Email = user.Email;
            existe.Role = user.Role;

            await _userRepository.Update(existe);
            return existe;
        }

        public async Task<User?> Delete(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return null;

            await _userRepository.Delete(id);
            return user;
        }
    }
}