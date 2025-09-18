using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // Queue for Push Notifications
        channel.ExchangeDeclare("payment_events", ExchangeType.Fanout);

        //auto generated bound que to payments events
        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName, exchange: "payment_events", routingKey: "");

        Console.WriteLine("*] PaymentConsumer waiting for payment_request events...");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            Console.WriteLine($" [PaymentConsumer] Creating payment record: {message}");
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        
        Console.ReadLine();
    }
}
