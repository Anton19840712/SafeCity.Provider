using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SafeCity.BrokerProducer;
using SafeCity.Business.Models.Response;

namespace SafeCity.BrokerConsumer;

public class ConsumerService(IProducerService producerService) : IConsumerService
{
	public string ConsumeMessage(IModel channel, IBasicProperties properties, string queueName)
	{
		var returnItem = string.Empty;
		var consumer = new EventingBasicConsumer(channel);
		consumer.Received += (sender, e) =>
		{
			Console.WriteLine();
			Console.Error.WriteLine("Start consuming:");
			Console.WriteLine();

			try
			{
				returnItem = Encoding.UTF8.GetString(e.Body.Span);

				Console.WriteLine($"Consumer: Received message: {returnItem}");
				channel.BasicAck(e.DeliveryTag, false);

				var receivedMessage = (Card112ChangedResponse)JsonConvert.DeserializeObject(returnItem, typeof(Card112ChangedResponse))!;

				//do something with received message from integration broker
				//maybe store to the database
				//producerService.Publish(resultOut, channel, properties, "ufm-out-queue");
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
				channel.BasicNack(e.DeliveryTag, false, false);
			}
		};

		try
		{
			channel.BasicConsume(
				queue: queueName,
				autoAck: false,
				consumer: consumer);
		}
		catch (Exception)
		{
			Console.Error.WriteLineAsync($"Failed to consume from {queueName}.");
		}

		//connection.Close();
		//connection.Dispose();
		return string.Empty;
	}
}