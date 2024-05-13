using System.ComponentModel.DataAnnotations;
using Weather.Model;

namespace Weather.Dtos.User
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = String.Empty;
        public int Age { get; set; }
        public string Gender { get; set; }= String.Empty;
        public string Email { get; set; }=String.Empty;
        public string Password { get; set; }=String.Empty;



    }
}
