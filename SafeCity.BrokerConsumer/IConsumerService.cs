using RabbitMQ.Client;

namespace SafeCity.BrokerConsumer;

public interface IConsumerService
{
	string ConsumeMessage(
	IModel channel,
	IBasicProperties properties,
	string queueName);
}