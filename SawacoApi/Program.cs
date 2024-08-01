using Microsoft.EntityFrameworkCore;
using MQTTnet.Client;
using SawacoApi.Domain.Persistances.Context;
using SawacoApi.Domain.Persistances.Repositories;
using SawacoApi.Domain.Repositories;
using SawacoApi.Domain.Services;
using SawacoApi.Hubs;
using SawacoApi.Mapping;
using SawacoApi.MQTTClients;
using SawacoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins("localhost", "http://localhost:3003", "http://localhost:3004", "http://localhost:3005", "http://localhost:3006", "http://localhost:3007", "http://localhost:3008", "http://localhost:3009", "http://localhost:3010")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHostedService<ScadaHost>();
builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection("MqttOptions"));
builder.Services.AddSingleton<ManagedMqttClient>();
builder.Services.AddSingleton<SawacoApi.MQTTClients.Buffer>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<ILoggerRepository, LoggerRepository>();
builder.Services.AddScoped<IStolenLineService, StolenLineService>();
builder.Services.AddScoped<IStolenLineRepository, StolenLineRepository>();

builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile));

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationHub>("/NotificationHub");
app.MapControllers();

app.Run();
