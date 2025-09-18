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

        // 1. Listen to user_events exchange
        channel.ExchangeDeclare(exchange: "user_events", type: ExchangeType.Fanout);

        var queueName = channel.QueueDeclare().QueueName; // auto-generated queue
        channel.QueueBind(queue: queueName,
                          exchange: "user_events",
                          routingKey: "");

        Console.WriteLine(" [EmailConsumer] Waiting for user events...");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            Console.WriteLine($" [EmailConsumer] Received: {message}");

            // Simulate sending email
            Console.WriteLine($" [EmailConsumer] Sending welcome email...");

            // 2. After sending, publish new event to payment_events
            channel.ExchangeDeclare(exchange: "payment_events", type: ExchangeType.Fanout);
            string paymentMessage = $"email.sent: {message}";
            var successBody = Encoding.UTF8.GetBytes(paymentMessage);

            channel.BasicPublish(exchange: "payment_events",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: successBody);

            Console.WriteLine($" [EmailConsumer] Published: {paymentMessage}");
        };

        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

        Console.ReadLine();
    }
}
