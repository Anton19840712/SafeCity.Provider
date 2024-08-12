using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using SafeCity.Business.Models.Common;
using SafeCity.Business.Models.Request;

public class BackgroundWorkerService : BackgroundService
{
	private readonly IConfiguration _configuration;
	private readonly HttpClient _httpClient;
	private readonly string _address;
	private readonly int _requestFrequency;
	private readonly string _gatewayTag;
	private readonly string _authToken;
	private readonly ILogger<BackgroundWorkerService> _logger;
	private int _currentId; // Поле для хранения текущего значения Id
	private int _nameCounter; // Поле для хранения счетчика имен

	public BackgroundWorkerService(IConfiguration configuration, ILogger<BackgroundWorkerService> logger)
	{
		_configuration = configuration;
		_logger = logger;

		_httpClient = new HttpClient();

		_address = _configuration["Address"];
		_requestFrequency = int.Parse(_configuration["RequestFrequency"] ?? "5000");
		_gatewayTag = _configuration["GatewayTag"];
		_authToken = _configuration["Authentication:Token"];

		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);

		_currentId = 0; // Начальное значение для Id
		_nameCounter = 0; // Начальное значение для счетчика имен
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			_logger.LogInformation("Sending request to {address} at: {time}...", _address, DateTimeOffset.Now);

			await SendRequestAsync();

			await Task.Delay(_requestFrequency, stoppingToken);
		}
	}

	private async Task SendRequestAsync()
	{
		try
		{
			var card = new Card112ChangedRequest
			{
				GlobalId = Guid.NewGuid().ToString(),
				NEmergencyCardId = _currentId,
				DtCreate = DateTime.UtcNow,
				StrCgPN = $"CPN-{_nameCounter}",
				StrAddressString = $"123 Main St, Apt {_nameCounter}",
				StrBuilding = $"Building {_nameCounter}",
				StrCorps = $"Corps {_nameCounter}",
				StrRoom = $"Room {_nameCounter}",
				StrAdditionalLocationInfo = $"Additional Info {_nameCounter}",
				StrIncidentDescription = $"Incident description {_nameCounter}",
				NIncidentTypeID = _nameCounter % 10,
				StrIncidentType = $"Type {_nameCounter}",
				StrCallerContactPhone = $"+1-800-{1000 + _nameCounter}",
				StrDeclarantName = $"Name {_nameCounter}",
				StrDeclarantLastName = $"LastName {_nameCounter}",
				StrDeclarantMiddleName = $"MiddleName {_nameCounter}",
				StrDeclarantBuildingNumber = $"{_nameCounter}",
				StrDeclarantAddressString = $"Declarant Address {_nameCounter}",
				StrDeclarantCorps = $"Declarant Corps {_nameCounter}",
				StrDeclarantFlat = $"Flat {_nameCounter}",
				GeoLatitude = $"{40.7128 + (_nameCounter % 10) * 0.01}",
				GeoLongitude = $"{-74.0060 + (_nameCounter % 10) * 0.01}",
				DeclarantGeoLatitude = $"{40.7128 + (_nameCounter % 10) * 0.01}",
				DeclarantGeoLongitude = $"{-74.0060 + (_nameCounter % 10) * 0.01}",
				CallId = $"Call-{_nameCounter}",
				NCasualties = _nameCounter % 5,
				StrStreetKLADR = $"StreetKLADR {_nameCounter}",
				StrDeclarantStreetKLADR = $"DeclarantStreetKLADR {_nameCounter}",
				StrDistrictKLADR = $"DistrictKLADR {_nameCounter}",
				StrDeclarantDistrictKLADR = $"DeclarantDistrictKLADR {_nameCounter}",
				StrCityKLADR = $"CityKLADR {_nameCounter}",
				StrDeclarantCityKLADR = $"DeclarantCityKLADR {_nameCounter}",
				StrOperatorName = $"Operator {_nameCounter}",
				LWithCall = $"WithCall {_nameCounter}",
				StrOKTMO = $"OKTMO {_nameCounter}",
				EraGlonassData = new EraGlonassData
				{
					// Здесь можно добавить данные для EraGlonassData
				},
				CallHistory = new CallHistory
				{
					// Здесь можно добавить данные для CallHistory
				},
				StrLatestModifier = $"Modifier {_nameCounter}"
			};

			_currentId++; // Увеличиваем Id на 1 при каждой отправке
			_nameCounter++; // Увеличиваем счетчик имен на 1 при каждой отправке

			string jsonContent = JsonSerializer.Serialize(card);

			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _httpClient.PostAsync(_address, content);
			if (response.IsSuccessStatusCode)
			{
				_logger.LogInformation("Request to {address} succeeded at: {time}", _address, DateTimeOffset.Now);
			}
			else
			{
				_logger.LogWarning("Request to {address} failed with status: {statusCode} at: {time}", _address, response.StatusCode, DateTimeOffset.Now);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError("Error occurred when sending request to {address}: {message}", _address, ex.Message);
		}
	}
}
