using System;
using System.ComponentModel.DataAnnotations;

namespace SMMP.Core.Models.Authentication
{
    public class User
    {
        public int Id { get; set; }

        public Guid Identifier { get; private set; }

        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string Salt { get; protected set; }

        public bool IsActive { get; private set; }

        public string UserRole { get; set; }

        public UserProfile Profile { get; set; }

        public User() { }

        public User(string email, string password, string salt, string userRole)
        {
            Identifier = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Password = password;
            Salt = salt;
            IsActive = true;
            UserRole = userRole;
            Profile = new UserProfile(this);
        }

        public void SetEmail(string email)
        {
            var emailValidator = new EmailAddressAttribute();

            if (emailValidator.IsValid(email))
            {
                Email = email;
            }
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetSalt(string salt)
        {
            Salt = salt;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void DisActivate()
        {
            IsActive = false;
        }
    }
}
