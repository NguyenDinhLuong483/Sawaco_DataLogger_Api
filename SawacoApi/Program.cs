
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins("localhost", "http://localhost:3000", "http://localhost:3004", "http://localhost:3005", "http://localhost:3006", "http://localhost:3007", "http://localhost:3008", "http://localhost:3009", "http://localhost:3010")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SawacoApi.API"))
);

builder.Services.AddHostedService<ScadaHost>();
builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection("MqttOptions"));
builder.Services.AddSingleton<ManagedMqttClient>();
builder.Services.AddSingleton<SawacoApi.Intrastructure.MQTTClients.Buffer>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGPSDeviceService, GPSDeviceService>();
builder.Services.AddScoped<IGPSDeviceRepository, GPSDeviceRepository>();
builder.Services.AddScoped<IStolenLineService, StolenLineService>();
builder.Services.AddScoped<IStolenLineRepository, StolenLineRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IGPSObjectService, GPSObjectService>();
builder.Services.AddScoped<IGPSObjectRepository, GPSObjectRepository>();

builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile));

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

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
