using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SafeCity.Business.Models.Request;

namespace SafeCity.BrokerProducer;

public class ProducerService : IProducerService
{
	public void Publish(Card112ChangedRequest message, IModel channel, IBasicProperties properties, string routingKeyValue)
	{
		if (message != null)
		{
			var json2 = JsonConvert.SerializeObject(message, Formatting.Indented);

			var body = Encoding.UTF8.GetBytes(json2);

			channel.BasicPublish(
				mandatory: true,
				basicProperties: properties,
				exchange: string.Empty,
				routingKey: routingKeyValue,
				body: body);

			Console.WriteLine();
			Console.WriteLine($"Publisher: Message was published {json2}.");
		}
		else
		{
			Console.WriteLine();
			Console.WriteLine($"Publisher: Message was null.");
		}
	}
}