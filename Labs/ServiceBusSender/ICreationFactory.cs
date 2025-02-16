namespace ServiceBusSender
{
    public interface ICreationFactory
    {
        void Register<T>(Func<string, string, Task<IPublisher>> creator) where T : class;
        Task<IPublisher> Get<T>(string connectionString, string topicOrQueue) where T : class;
    }
}
