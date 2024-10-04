using SawacoApi.Host.Hosting;
using SawacoApi.Intrastructure.Context;
using SawacoApi.Intrastructure.Mapping;
using SawacoApi.Intrastructure.MQTTClients;
using SawacoApi.Intrastructure.Repositories.Loggers;
using SawacoApi.Intrastructure.Repositories.StolenLines;
using SawacoApi.Intrastructure.Repositories.UnitOfWork;
using SawacoApi.Intrastructure.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging();
        });
        services.AddHostedService<HostWorker>();
        var config = builder.Configuration;
        services.Configure<MqttOptions>(config.GetSection("MqttOptions"));
        services.AddSingleton<ManagedMqttClient>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddScoped<ILoggerRepository, LoggerRepository>();
        services.AddScoped<IStolenLineService, StolenLineService>();
        services.AddScoped<IStolenLineRepository, StolenLineRepository>();

        services.AddWindowsService(options =>
        {
            options.ServiceName = "Scada Host";
        });
        services.AddAutoMapper(typeof(ModelToViewModelProfile));
    })
    .Build();

await host.RunAsync();
