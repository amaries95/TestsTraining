using Microsoft.EntityFrameworkCore;
using TestsTraining.Domain.Entities;

namespace TestsTraining.Data
{
    public class ProjectProjectDbContext : DbContext
    {
        public virtual DbSet<User?> Users { get; set; }

        public ProjectProjectDbContext(DbContextOptions<ProjectProjectDbContext> options) : base(options) { }
    }
}
