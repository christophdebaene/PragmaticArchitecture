namespace SlickBus
{
    public interface IMediator
    {
        TResponse Send<TResponse>(IRequest<TResponse> request);
    }
}