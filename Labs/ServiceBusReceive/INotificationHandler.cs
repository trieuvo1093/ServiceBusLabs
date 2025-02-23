using Azure.Messaging.ServiceBus;

namespace ServiceBusConsumer
{
    public interface INotificationHandler
    {
        Task Handle(IReceiver receiver, List<ServiceBusReceivedMessage> serviceBusReceivedMessages, CancellationToken cancellationToken);
    }

    public class TestNotificationHandler : INotificationHandler
    {
        public async Task Handle(IReceiver receiver, List<ServiceBusReceivedMessage> serviceBusReceivedMessages, CancellationToken cancellationToken)
        {
            foreach (ServiceBusReceivedMessage receivedMessage in serviceBusReceivedMessages)
            {
                // get the message body as a string
                string body = receivedMessage.Body.ToString();

                await receiver.CompleteMessageAsync(receivedMessage, cancellationToken);
            }
        }
    }
}
