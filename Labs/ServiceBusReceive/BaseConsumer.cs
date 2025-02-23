using Azure.Messaging.ServiceBus;

namespace ServiceBusConsumer
{
    public abstract class BaseConsumer : IConsumer
    {
        protected readonly INotificationHandler _handler;
        protected readonly BaseConsumerConfiguration _config;

        protected readonly ServiceBusClient _client;
        protected NonSessionReceiver? _receiver;


        protected BaseConsumer(BaseConsumerConfiguration config, INotificationHandler handler, ServiceBusRetryOptions retryOptions)
        {
            _handler = handler;
            _config = config;
            var options = new ServiceBusClientOptions();
            if (retryOptions != null)
            {
                options.RetryOptions = retryOptions;
            }
            _client = new ServiceBusClient(_config.ConnectionString, options);
        }

        protected abstract NonSessionReceiver CreateReceiver();
        protected abstract Task<SessionReceiver> CreateSessionReceiver(CancellationToken cancellationToken);

        public virtual async Task ConsumerMessageAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var messages = await _receiver!.ReceiveMessagesAsync(_config.BatchCount, TimeSpan.FromMilliseconds(100));
                if (messages.Any())
                {
                    await _handler.Handle(_receiver, messages.ToList(), cancellationToken)!;
                }
            }
        }

        public virtual async Task ConsumeSessionMessageAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var sessionReceiver = await CreateSessionReceiver(cancellationToken);

                while (!cancellationToken.IsCancellationRequested)
                {
                    var messages = await sessionReceiver.ReceiveMessagesAsync(_config.BatchCount, TimeSpan.FromMilliseconds(1000), cancellationToken);

                    if (messages.Any())
                    {
                        await _handler.Handle(sessionReceiver, messages.ToList(), cancellationToken)!;
                    }

                    await sessionReceiver.CloseAsync(messages, cancellationToken);
                }
            }
        }


    }
}
