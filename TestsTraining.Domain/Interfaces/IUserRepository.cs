using TestsTraining.Domain.Entities;

namespace TestsTraining.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);

        Task AddNewUser(User user);

        Task<List<User?>> GetAllUsers();
    }
}
