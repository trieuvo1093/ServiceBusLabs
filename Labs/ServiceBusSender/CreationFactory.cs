namespace ServiceBusSender
{
    public class CreationFactory : ICreationFactory
    {
        private Dictionary<Type, Func<string, string, Task<IPublisher>>> _publishers = [];

        public void Register<T>(Func<string, string, Task<IPublisher>> creator) where T : class
        {
            if (_publishers.ContainsKey(typeof(T)))
            {
                throw new IndexOutOfRangeException("Cannot register the same message type twice");
            }
            _publishers.Add(typeof(T), creator);
        }

        public Task<IPublisher> Get<T>(string connectionString, string topicOrQueue) where T : class
        {
            if (_publishers.TryGetValue(typeof(T), out var publisher))
            {
                return publisher.Invoke(connectionString, topicOrQueue);
            }
            else
            {
                return Task.FromResult<IPublisher>(new Publisher(connectionString, topicOrQueue));
            }
        }
    }
}
