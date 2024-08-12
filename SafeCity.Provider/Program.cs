using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = new ConfigurationBuilder();
BuildConfig(builder);

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Build())
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.CreateLogger();

Log.Logger.Information("Application starting");

var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((context, services) =>
	{
		// Регистрируем Serilog как ILogger
		services.AddSingleton(Log.Logger);
		services.AddHostedService<BackgroundWorkerService>();
	})
	.UseSerilog() // Используем Serilog для логирования в хосте
	.Build();

await host.RunAsync();

void BuildConfig(IConfigurationBuilder builder)
{
	builder.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
		.AddEnvironmentVariables();
}

