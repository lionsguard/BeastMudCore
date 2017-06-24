using System;

namespace Beast.Security
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string RealName { get; set; }
        string Password { get; set; }
        string PasswordSalt { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime LastLoginDate { get; set; }
        bool IsLockedOut { get; set; }
        bool IsDeactivated { get; set; }
        bool IsEmailVerified { get; set; }
    }
}
