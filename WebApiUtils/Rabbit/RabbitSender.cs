using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace WebApiUtils.Rabbit
{
    public class RabbitSender
    {
        public string Queue { get; private set; }

        public RabbitSender(string queue)
        {
            Queue = queue;
        }

        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = RabbitConfig.Server,
                Port = RabbitConfig.Port,
                UserName = RabbitConfig.User,
                Password = RabbitConfig.Password,
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: Queue, basicProperties: null, body: body);
                }
            }
        }
    }
}