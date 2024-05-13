namespace Weather.Dtos.User
{
    public class CreateUserRequestDto
    {
        public string UserName { get; set; }=String.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = String.Empty;
        public string Email { get; set; }= String.Empty;
        public string Password { get; set; } = String.Empty;

    }
}
