using Azure.Messaging.ServiceBus;
using ServiceBusCommon.Constants;

namespace ServiceBusSender
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Service Bus!");

            var creationFactory = new CreationFactory();
            creationFactory.Register<string>(CreatePublisher);

            var publisher = await creationFactory.Get<string>(ServiceBusConstants.ConnectionString, "queuetesta");

            await publisher.PublishAsync<string>("Test1");
        }

        static async Task<IPublisher> CreatePublisher(string connectionString, string topic)
        {
            return await Task.FromResult(new Publisher(connectionString, topic));
        }
    }
}
