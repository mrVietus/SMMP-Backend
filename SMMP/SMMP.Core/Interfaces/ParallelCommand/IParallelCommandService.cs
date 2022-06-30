using System;
using SMMP.Core.Mediator;

namespace SMMP.Core.Interfaces.ParallelCommand
{
    public interface IParallelCommandService
    {
        void ProcessCommandMessagePararelly(ParallelCommandBase command);
    }
}
