namespace EventBus.RabbitMq;
public class RabbitMqReceiver : BackgroundService
{
	private IConnection connection;
	private IModel channel;

	public RabbitMqReceiver()
	{
		var factory = new ConnectionFactory 
		{
			HostName = "localhost" 
		};

		connection = factory.CreateConnection();
		channel = connection.CreateModel();
		channel.QueueDeclare(queue: "Orders", 
							durable: false, 
							exclusive: false, 
							autoDelete: false, 
							arguments: null);
	}

	protected override Task ExecuteAsync(CancellationToken cancellationToken)
	{
		var consumer = new EventingBasicConsumer(channel);

		consumer.Received += (model, ea) =>
		{
			var content = Encoding.UTF8.GetString(ea.Body.ToArray());

			Debug.WriteLine($"Received messsage: {content}");

			channel.BasicAck(ea.DeliveryTag, false);
		};

		channel.BasicConsume("Orders", false, consumer);

		return Task.CompletedTask;
	}
}
