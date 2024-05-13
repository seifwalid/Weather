using System.ComponentModel.DataAnnotations;

namespace Weather.Dtos.User
{
    public class CreateUserWithGoogle
    {
        [Key]
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
