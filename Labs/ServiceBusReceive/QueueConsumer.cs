namespace ServiceBusConsumer
{
    public class QueueConsumer : BaseConsumer
    {
        private readonly string _queueName;

        public QueueConsumer(QueueConsumerConfiguration config, INotificationHandler handler) : base(config, handler, config.RetryOptions)
        {
            _queueName = config.QueueName;

            if (!_config.RequiresSession)
            {
                _receiver = CreateReceiver();
            }
        }

        protected override NonSessionReceiver CreateReceiver()
        {
            return new NonSessionReceiver(_client.CreateReceiver(_queueName));
        }

        protected override async Task<SessionReceiver> CreateSessionReceiver(CancellationToken cancellationToken)
        {
            var serviceBusReceiver = await _client.AcceptNextSessionAsync(_queueName, cancellationToken: cancellationToken);
            return new SessionReceiver(serviceBusReceiver);
        }
    }
}
