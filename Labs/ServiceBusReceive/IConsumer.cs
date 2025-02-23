namespace ServiceBusConsumer
{
    public interface IConsumer
    {
        Task ConsumerMessageAsync(CancellationToken cancellationToken);
        Task ConsumeSessionMessageAsync(CancellationToken cancellationToken);
    }
}
