using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebApiUtils.BaseApi;
using WebApiUtils.Entities;

namespace WebApiUtils.Rabbit
{
    public class CloseRentRabbitListener : BaseRabbitListener
    {
        protected string connectionString => Environment.GetEnvironmentVariable("ConnectionString")!;
        protected BaseRepository<DBookRent> repository => new BaseRepository<DBookRent>(connectionString);

        public CloseRentRabbitListener() : base(RabbitConfig.CloseRentQueue) { }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var input = JsonSerializer.Deserialize<DBookRentLinked>(content);

                // Для того, чтобы сущность обновилась не сразу
                Task.Delay(1000).Wait();

                if (input is not null)
                {
                    var item = repository.GetById(input.Id);
                    if (item is not null)
                    {
                        item.CloseDate = input.CloseDate;
                        item.Penalty = input.Penalty;

                        repository.Update(item);
                    }
                }
            };

            channel.BasicConsume(RabbitConfig.CloseRentQueue, true, consumer);

            return Task.CompletedTask;
        }
    }
}
