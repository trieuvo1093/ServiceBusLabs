using Azure.Messaging.ServiceBus;

namespace ServiceBusConsumer
{
    public class SessionReceiver : IReceiver
    {
        private readonly ServiceBusSessionReceiver _receiver;
        public SessionReceiver(ServiceBusSessionReceiver receiver)
        {
            _receiver = receiver;
        }

        public async Task CompleteMessageAsync(ServiceBusReceivedMessage msg, CancellationToken ct)
        {
            await _receiver.CompleteMessageAsync(msg, ct);
        }

        public async Task DeadLetterMessageAsync(ServiceBusReceivedMessage msg, string reason, string description, CancellationToken ct)
        {
            await _receiver.DeadLetterMessageAsync(msg, reason, description, ct);
        }

        public async Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default)
        {
            return await _receiver.ReceiveMessagesAsync(maxMessages, maxWaitTime, cancellationToken);
        }

        internal async Task CloseAsync(IReadOnlyList<ServiceBusReceivedMessage> messages, CancellationToken cancellationToken = default)
        {
            if (messages.Count == 0 || _receiver.SessionLockedUntil < DateTimeOffset.UtcNow)
            {
                await _receiver.CloseAsync(cancellationToken);
            }
        }
    }
}
