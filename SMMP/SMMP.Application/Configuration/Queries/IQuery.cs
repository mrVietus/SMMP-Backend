using System;
using MediatR;

namespace SMMP.Application.Configuration.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
