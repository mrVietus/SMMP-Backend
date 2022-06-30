using System;
using SMMP.Core.Models;

namespace SMMP.Core.Mediator
{
    public abstract class ParallelCommandBase : CommandBase
    {
        protected ParallelCommandBase(Execution execution)
        {
            Execution = execution;
        }

        public Execution Execution { get; set; }
    }
}
