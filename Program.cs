using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HotelManagementAPI.Data;
using HotelManagementAPI.Helper;
using Microsoft.EntityFrameworkCore;
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
	options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
});

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
