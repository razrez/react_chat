using System.Text;
using System.Text.Json;
using Amazon.S3.Model;
using RabbitMQ.Client;

namespace Chat.API.Publisher;

public class MessagePublisher : IMessagePublisher
{
    public void SaveMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.ExchangeDeclare("logs", ExchangeType.Fanout);
        channel.QueueDeclare(queue: "message-queue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        channel.BasicPublish(exchange: "",
            routingKey: "message-queue",
            basicProperties: null,
            body: body);
    }


    public void UploadFileOrMeta<T>(T data, string queueName)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.ExchangeDeclare("logs", ExchangeType.Fanout);
        channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var jsonMessage = JsonSerializer.Serialize(data);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        channel.BasicPublish(exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body);
    }
}