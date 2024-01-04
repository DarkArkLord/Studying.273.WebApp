using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace WebApiUtils.Rabbit
{
    public abstract class BaseRabbitListener : BackgroundService
    {
        public string Queue { get; private set; }

        protected IConnection connection;
        protected IModel channel;

        public BaseRabbitListener(string queue)
        {
            Queue = queue;

            var factory = new ConnectionFactory()
            {
                HostName = RabbitConfig.Server,
                Port = RabbitConfig.Port,
                UserName = RabbitConfig.User,
                Password = RabbitConfig.Password,
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(queue: Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public override void Dispose()
        {
            channel.Close();
            connection.Close();
            base.Dispose();
        }
    }
}
