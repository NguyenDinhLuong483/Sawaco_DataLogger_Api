
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
