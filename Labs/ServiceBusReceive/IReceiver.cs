using Azure.Messaging.ServiceBus;

namespace ServiceBusConsumer
{
    public interface IReceiver
    {
        Task DeadLetterMessageAsync(ServiceBusReceivedMessage msg, string reason, string description, CancellationToken ct);

        Task CompleteMessageAsync(ServiceBusReceivedMessage msg, CancellationToken ct);
    }
}
