using System;
using MediatR;

namespace SMMP.Core.Mediator
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
