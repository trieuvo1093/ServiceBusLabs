namespace ServiceBusConsumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                Console.WriteLine("Cancellation requested...");
                eventArgs.Cancel = true; // Prevent immediate shutdown
                cts.Cancel(); // Signal cancellation to running tasks
            };
            Console.WriteLine("Hello, World!");

            var config = new QueueConsumerConfiguration
            {
                ConnectionString = ServiceBusCommon.Constants.ServiceBusConstants.ConnectionString,
                BatchCount = 1,
                QueueName = "queuetesta",
                RequiresSession = false
            };
            var notificationHandler = new TestNotificationHandler();
            var consumer = new QueueConsumer(config, notificationHandler);
            await consumer.ConsumerMessageAsync(cts.Token);
        }
    }
}
