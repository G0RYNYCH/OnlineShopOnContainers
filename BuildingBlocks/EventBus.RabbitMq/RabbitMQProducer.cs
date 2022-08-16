namespace EventBus.RabbitMq;
public class RabbitMqProducer : IMessageProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "Orders",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                            routingKey: "Orders",
                            basicProperties: null,
                            body: body);
        }
    }
}
