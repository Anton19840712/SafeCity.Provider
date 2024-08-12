using RabbitMQ.Client;

namespace SafeCity.BrokerConnection;

public class RabbitConnectionService : IRabbitConnectionService
{
	private readonly IConnectionFactory _connectionFactory;
	private readonly IConnection _connection;
	private readonly IModel _channel;

	public RabbitConnectionService()
	{
		_connectionFactory = new ConnectionFactory()
		{
			HostName = "localhost",
			UserName = "guest",
			Password = "guest",
			VirtualHost = "/"
		};

		_connection = _connectionFactory.CreateConnection();
		_channel = _connection.CreateModel();
	}
	public IConnection GetConnection() => _connection;
	public IModel GetChannel() => _channel;

	public void DisposeConnection()
	{
		if (_connection.IsOpen)
		{
			_connection.Close();
			_connection.Dispose();
		}
	}
}