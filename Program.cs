using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HotelManagementAPI.Data;
using HotelManagementAPI.Helper;
using HotelManagementAPI.Services.IService;
using HotelManagementAPI.Services.IServices;
using HotelManagementAPI.Services.Service;
using HotelManagementAPI.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Sieve.Services;

var builder = WebApplication.CreateBuilder(args);
// Inject serilog
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//inject unit of work
//inject automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//inject sieve
builder.Services.AddSingleton<SieveProcessor>();
//inject helper
builder.Services.AddSingleton<IHelper, Helper>();
builder.Services.AddScoped<IDatPhongService, DatPhongService>();
builder.Services.AddScoped<IHoaDonService, HoaDonService>();
builder.Services.AddScoped<IThemPhuPhiService, ThemPhuPhiService>();
builder.Services.AddScoped<IThemDichVuService, ThemDichVuService>();
//jnject cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
// Connect Azure Key Vault to inject secrets
string kvURL = builder.Configuration["KeyVaultConfig:KeyVaultURL"];
string tenantID = builder.Configuration["KeyVaultConfig:TenantID"];
string clientID = builder.Configuration["KeyVaultConfig:ClientID"];
string clientSecret = builder.Configuration["KeyVaultConfig:ClientSecret"];
var credential = new ClientSecretCredential(tenantID, clientID, clientSecret);
var client = new SecretClient(new Uri(kvURL), credential);
// Inject Key Vault secrets to env
builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// connect to SQL Server
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
