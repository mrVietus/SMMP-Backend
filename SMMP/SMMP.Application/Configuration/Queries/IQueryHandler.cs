using System;
using MediatR;

namespace SMMP.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IRequest<TResult>
    {
    }
}
