using SafeCity.BrokerConnection;
using SafeCity.BrokerConsumer;
using SafeCity.BrokerProducer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Настройка Serilog до создания WebApplication
Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(); // Используем Serilog для логирования

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddHostedService<BackgroundWorkerService>();
services.AddTransient<IRabbitConnectionService, RabbitConnectionService>();
services.AddTransient<IProducerService, ProducerService>();
services.AddTransient<IConsumerService, ConsumerService>();


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
