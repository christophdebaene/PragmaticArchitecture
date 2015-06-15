using System;

namespace SlickBus
{
    public abstract class Request<TResponse> : IRequest<TResponse>
    {
        public Guid Id { get; private set; }

        public Request()
        {
            Id = Guid.NewGuid();
        }

        public Request(Guid id)
        {
            Id = id;
        }
    }
}