using Azure.Messaging.ServiceBus;

namespace ServiceBusConsumer
{
    public class NonSessionReceiver : IReceiver
    {
        private readonly ServiceBusReceiver _receiver;

        public NonSessionReceiver(ServiceBusReceiver receiver)
        {
            _receiver = receiver;
        }

        public Task CompleteMessageAsync(ServiceBusReceivedMessage msg, CancellationToken ct)
        {
            return _receiver.CompleteMessageAsync(msg, ct);
        }

        public Task DeadLetterMessageAsync(ServiceBusReceivedMessage msg, string reason, string description, CancellationToken ct)
        {
            return _receiver.DeadLetterMessageAsync(msg, reason, description, ct);
        }

        public Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default)
        {
            return _receiver.ReceiveMessagesAsync(maxMessages, maxWaitTime, cancellationToken);
        }
    }
}
