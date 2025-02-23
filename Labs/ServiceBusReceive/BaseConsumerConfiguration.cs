using Azure.Messaging.ServiceBus;

namespace ServiceBusConsumer
{
    public abstract class BaseConsumerConfiguration
    {
        public string ConnectionString { get; set; }
        public int BatchCount { get; set; }
        public bool RequiresSession { get; set; } = false;
        public ServiceBusRetryOptions RetryOptions { get; set; }
    }
}
