using Microsoft.EntityFrameworkCore;
using TestsTraining.Data.Helpers;
using TestsTraining.Domain.Entities;
using TestsTraining.Domain.Interfaces;

namespace TestsTraining.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectProjectDbContext _projectProjectDbContext;

        public UserRepository(ProjectProjectDbContext projectProjectDbContext)
        {
            _projectProjectDbContext = projectProjectDbContext;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _projectProjectDbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User?>> GetAllUsers()
        {
            return await _projectProjectDbContext.Users.ToListAsync();
        }

        public async Task AddNewUser(User createUserRequest)
        {
            createUserRequest.UserName = RepositoryHelper.ConvertEmailToUsername(createUserRequest.Email);
            _projectProjectDbContext.Users.Add(createUserRequest);

            await _projectProjectDbContext.SaveChangesAsync();
        }

        private async Task<int> GetNumberOfEntries()
        {
            return await _projectProjectDbContext.Users.CountAsync();
        }
    }
}
