using System;
using SMMP.Core.Models;

namespace SMMP.Core.Mediator
{
    public abstract class CommandBase : ICommand
    {
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
    }

    public abstract class AsyncCommandBase : CommandBase
    {
        protected AsyncCommandBase(Execution execution)
        {
            Execution = execution;
        }

        public Execution Execution { get; set; }
    }
}
