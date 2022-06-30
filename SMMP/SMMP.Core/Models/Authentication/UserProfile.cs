using System;
using SMMP.Core.Models.Enums;

namespace SMMP.Core.Models.Authentication
{
    public class UserProfile
    {
        public int Id { get; set; }

        public Guid Identifier { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public UserStatus Status { get; set; }

        public string PhotoUrl { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public UserProfile() { }

        public UserProfile(User user)
        {
            Identifier = Guid.NewGuid();
            Created = DateTime.UtcNow;
            Status = UserStatus.Incomplete;
            User = user;
        }
    }
}
