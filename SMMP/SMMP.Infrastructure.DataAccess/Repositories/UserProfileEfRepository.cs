using System;
using SMMP.Core.Interfaces.Repositories;
using SMMP.Core.Models.Authentication;
using SMMP.Infrastructure.DataAccess.Repositories.Base;
using SMMP.Infrastructure.Database;

namespace SMMP.Infrastructure.DataAccess.Repositories
{
    public class UserProfileEfRepository : EntityFrameworkRepository<UserProfile>, IUserProfileEfRepository
    {
        public UserProfileEfRepository(ApplicationContext context)
            : base(context)
        {
        }
    }
}
