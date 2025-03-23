using AccountManagementAPI.Configuration;
using AccountManagementAPI.Domains.Accounts;
using AccountManagementAPI.Domains.Persons;
using AccountManagementAPI.Domains.Transactions;
using AccountManagementAPI.Helpers;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Enrichers.Span;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
    loggerConfiguration.Enrich.With<ActivityEnricher>();
    loggerConfiguration.Enrich.WithProperty("Application Name", "Account Management API");
});

builder.Services.Configure<ConnectionStringOptions>(
    builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.Configure<StoredProcedureOptions>(
    builder.Configuration.GetSection("StoredProcedures"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    void AddSwaggerDoc(string name, string title, string description)
    {
        setup.SwaggerDoc(name, new OpenApiInfo
        {
            Title = title,
            Description = description,
            Version = SwaggerSetupConstants.Version,
            Contact = new OpenApiContact
            {
                Name = SwaggerSetupConstants.name,
                Email = SwaggerSetupConstants.Email
            }
        });
    }

    AddSwaggerDoc("OpenApiSpecificationForPersons", "Account Management API [Persons]", "This API provides functionality to manage persons");
    AddSwaggerDoc("OpenApiSpecificationForAccounts", "Account Management API [Accounts]", "This API provides functionality to manage accounts for a person");
    AddSwaggerDoc("OpenApiSpecificationForTransactions", "Account Management API [Transactions]", "This API provides functionality to manage transactions for a account of a person");

}
);

builder.Services.AddScoped<Stopwatch>();
builder.Services.AddPersonsServices();
builder.Services.AddAccountsServices();
builder.Services.AddTransactionsServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint(
            url: "/swagger/OpenApiSpecificationForPersons/swagger.json",
            name: "Account Management API [Persons]");
        
        setup.SwaggerEndpoint(
            url: "/swagger/OpenApiSpecificationForAccounts/swagger.json",
            name: "Account Management API [Accounts]");
        
        setup.SwaggerEndpoint(
            url: "/swagger/OpenApiSpecificationForTransactions/swagger.json",
            name: "Account Management API [Transactions]");
    });

    app.Map("/", HttpContext => Task.Run(() => HttpContext.Response.Redirect("/swagger"))).ShortCircuit();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
