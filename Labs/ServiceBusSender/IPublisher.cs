namespace ServiceBusSender
{
    public interface IPublisher
    {
        Task PublishAsync<T>(T message, string? sessionId = null, Dictionary<string, object>? headers = null);
    }
}
