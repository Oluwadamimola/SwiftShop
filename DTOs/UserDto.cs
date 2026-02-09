using static SwiftShop.Models.User;

namespace SwiftShop.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set;}
    }

    public class UserRegisterDto
    {
        public string? Name { get; set;} 
        public string? Email { get; set;}
        public string? Password { get; set; }
        public string? DateTime { get; set; }
    }

    public class UserLoginDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class UserDeleteDto
    {
        public Guid Id { get; set; }
    }
}