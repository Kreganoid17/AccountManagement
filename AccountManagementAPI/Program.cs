using AccountManagementAPI.Configuration;
using AccountManagementAPI.Domains.Persons;
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

}
);

builder.Services.AddScoped<Stopwatch>();
builder.Services.AddPersonsServices();

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
    });

    app.Map("/", HttpContext => Task.Run(() => HttpContext.Response.Redirect("/swagger"))).ShortCircuit();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
