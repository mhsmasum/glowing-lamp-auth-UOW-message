using DotNetSecurity.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DotNetSecurity.Publisher
{
    public static class DirectExchangePublisher
    {

        public static void Publish(IModel channel , Order order ) 
        {
            var count = 0;
            channel.ExchangeDeclare("order-direct-exchange", ExchangeType.Direct);

            //while (true)
            //{
                //var messsage = new { Name = "Message Produced", Description = "Rabbit MQ 101 Message Queue From Producer", Count = count };
                var data = JsonSerializer.Serialize(order);
                var body = Encoding.UTF8.GetBytes(data);
                channel.BasicPublish("order-direct-exchange", "order.init", null, body);
                count++;
               
            //}
        }
    }
}
