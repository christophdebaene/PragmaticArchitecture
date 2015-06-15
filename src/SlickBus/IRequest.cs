using System;

namespace SlickBus
{
    public interface IRequest<out TResponse>
    {
        Guid Id { get; }
    }
}