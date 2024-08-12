using RabbitMQ.Client;
using SafeCity.Business.Models.Request;

namespace SafeCity.BrokerProducer;

public interface IProducerService
{
	void Publish(
		Card112ChangedRequest message,
		IModel channel,
		IBasicProperties properties,
		string routingKey);
}