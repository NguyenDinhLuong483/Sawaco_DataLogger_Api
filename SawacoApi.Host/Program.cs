
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SawacoApi.API"))
        );
        services.AddHostedService<HostWorker>();
        var config = builder.Configuration;
        services.Configure<MqttOptions>(config.GetSection("MqttOptions"));
        services.AddSingleton<ManagedMqttClient>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGPSDeviceService, GPSDeviceService>();
        services.AddScoped<IGPSDeviceRepository, GPSDeviceRepository>();
        services.AddScoped<IGPSObjectService, GPSObjectService>();
        services.AddScoped<IGPSObjectRepository, GPSObjectRepository>();
        services.AddScoped<IStolenLineService, StolenLineService>();
        services.AddScoped<IStolenLineRepository, StolenLineRepository>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IHistoryService, HistoryService>();
        services.AddScoped<IGPSHistoryRepository, GPSHistoryRepository>();
        

        services.AddWindowsService(options =>
        {
            options.ServiceName = "Scada Host";
        });
        services.AddAutoMapper(typeof(ModelToViewModelProfile));
    })
    .Build();

await host.RunAsync();
