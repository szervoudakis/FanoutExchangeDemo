using RabbitMQ.Client;
using System.Text;

namespace UserService.Services
{
    public class RabbitMqPublisher
    {
        private readonly ConnectionFactory _factory;


        public RabbitMqPublisher()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };   
        }
        //this function is responsible to publish registered user in user_events exchange        
        public void PublishUserRegistered(string username, string email)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("user_events", ExchangeType.Fanout);

            var message = $"New user registered: {username} ({email})";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "user_events", routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"Published: {message}");
        }
    }
}