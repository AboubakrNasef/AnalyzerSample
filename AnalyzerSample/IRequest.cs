namespace AnalyzerSample
{
	public interface IRequest<T>
	{
		T Value { get; set; }
	}
	public interface IRequestHandler<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
	{
		TResponse Handle(TRequest request);
	}
}
