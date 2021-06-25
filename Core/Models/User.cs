using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Models
{
    public enum Role
    {
        None,
        Merchendiser,
        Coordinator
    }

    public class User
    {
        [Required]
        public int Id { get; set; }

        public Role Role { get; }

        [Required]
        [StringLength(32)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32)]
        public string SecondName { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public User() : this(Role.None)
        {
        }

        public User(Role role)
        {
            Role = role;
        }
    }
}
