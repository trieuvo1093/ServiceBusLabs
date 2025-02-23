namespace ServiceBusConsumer
{
    public class TopicConsumerConfiguration : BaseConsumerConfiguration
    {
        public string TopicName { get; set; }
        public string Subscription { get; set; }
    }
}
