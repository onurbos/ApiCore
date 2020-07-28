

using System.ComponentModel.DataAnnotations;

namespace ApiCore.Models
{
    public class AddUserModel
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Picture { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }
    }
}
