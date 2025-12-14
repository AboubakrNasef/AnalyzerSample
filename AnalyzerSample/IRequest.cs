namespace AnalyzerSample
{
    public interface IRequest<T>
    {

    }
	public interface IRequestHandler<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
	{

	}
}
