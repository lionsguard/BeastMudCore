using System;

namespace Beast.Security
{
    public class User : IUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginDate { get; set; } = DateTime.UtcNow;
        public bool IsLockedOut { get; set; }
        public bool IsDeactivated { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
