using RabbitMQ.Client;

namespace SafeCity.BrokerConnection;

public interface IRabbitConnectionService
{
	void DisposeConnection();
	IModel GetChannel();
	IConnection GetConnection();
}