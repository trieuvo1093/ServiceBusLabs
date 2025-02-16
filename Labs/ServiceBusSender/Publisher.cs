using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace ServiceBusSender
{
    public class Publisher : IPublisher
    {
        protected readonly ServiceBusClient _serviceBusClient;
        protected readonly Azure.Messaging.ServiceBus.ServiceBusSender _serviceBusSender;

        public Publisher(string connectionString, string topicOrQueueName)
        {
            _serviceBusClient = new ServiceBusClient(connectionString);
            _serviceBusSender = _serviceBusClient.CreateSender(topicOrQueueName);
        }
        public Task PublishAsync<T>(T message, string? sessionId = null, Dictionary<string, object>? headers = null)
        {
            var serviceBusMessage = new ServiceBusMessage(JsonSerializer.Serialize(message));

            if (sessionId != null)
            {
                serviceBusMessage.SessionId = sessionId;
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    serviceBusMessage.ApplicationProperties.Add(header.Key, header.Value);
                }
            }

            return _serviceBusSender.SendMessageAsync(serviceBusMessage);
        }
    }
}
