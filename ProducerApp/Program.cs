using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //stable
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // fanout echange for user events
        channel.ExchangeDeclare("user_events", ExchangeType.Fanout);
        
        for (int i = 0; i <= 3; i++)
        {
            string message = $"New user registered: user{i}";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "user_events",
                routingKey: "",
                basicProperties: null,
                body: body);
                Console.WriteLine($" [Producer] Sent: {message}");
        }
        
    }
}
