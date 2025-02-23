namespace ServiceBusConsumer
{
    public class TopicConsumer : BaseConsumer
    {
        private readonly string _topicName;
        private readonly string _subscription;

        public TopicConsumer(TopicConsumerConfiguration config, INotificationHandler handler) : base(config, handler, config.RetryOptions)
        {
            _topicName = config.TopicName;
            _subscription = config.Subscription;

            if (!_config.RequiresSession)
            {
                _receiver = CreateReceiver();
            }
        }

        protected override NonSessionReceiver CreateReceiver()
        {
            return new NonSessionReceiver(_client.CreateReceiver(_topicName, _subscription));
        }

        protected override async Task<SessionReceiver> CreateSessionReceiver(CancellationToken cancellationToken)
        {
            var serviceBusReceiver = await _client.AcceptNextSessionAsync(_topicName, _subscription, cancellationToken: cancellationToken);
            return new SessionReceiver(serviceBusReceiver);
        }
    }
}
