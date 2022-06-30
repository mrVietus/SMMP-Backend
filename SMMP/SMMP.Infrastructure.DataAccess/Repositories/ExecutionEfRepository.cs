using System;
using SMMP.Core.Interfaces.Repositories;
using SMMP.Core.Models;
using SMMP.Infrastructure.DataAccess.Repositories.Base;
using SMMP.Infrastructure.Database;


namespace SMMP.Infrastructure.DataAccess.Repositories
{
    public class ExecutionEfRepository : EntityFrameworkRepository<Execution>, IExecutionEfRepository
    {
        public ExecutionEfRepository(ApplicationContext context)
            : base(context)
        {
        }
    }
}
