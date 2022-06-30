using System;
using MediatR;

namespace SMMP.Core.Mediator
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : IRequest
    {
    }

    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : IRequest<TResult>
    {
    }
}
