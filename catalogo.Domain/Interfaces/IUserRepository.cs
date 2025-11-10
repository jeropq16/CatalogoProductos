using catalogo.Domain.Models;

namespace catalogo.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task Create(User user);
    Task Update(User user);
    Task Delete(int id);
}
