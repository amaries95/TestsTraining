using System.ComponentModel.DataAnnotations.Schema;
using TestsTraining.Domain.Interfaces;

namespace TestsTraining.Domain.Entities
{
    public class User : IUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

    }
}
