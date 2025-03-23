using AccountManagement.ActionFilter;
using AccountManagement.Configuration;
using AccountManagement.Domains.Accounts;
using AccountManagement.Domains.Persons;
using AccountManagement.Domains.Transactions;
using HttpClientLibrary.HttpClientService;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Enrichers.Span;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

//var baseUrl = builder.Configuration["HttpClientApiUris:AccountManagementAPIAddress"] ?? throw new InvalidOperationException("The API configuration key is missing: AccountManagementAPIAddress");

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
    loggerConfiguration.Enrich.With<ActivityEnricher>();
    loggerConfiguration.Enrich.WithProperty("Application Name", "Account Management");
});

builder.Services.AddControllers(configure =>
{
    //configure.Filters.Add(new ProducesResponseTypeAttribute<ProblemDetails>(StatusCodes.Status401Unauthorized));
    configure.Filters.Add(new ProducesResponseTypeAttribute<ProblemDetails>(StatusCodes.Status500InternalServerError));
    configure.Filters.Add<ActionTimerAttribute>();
});

var apiBaseAddress = builder.Configuration["HttpClientApiUris:AccountManagementAPIAddress"];

builder.Services.Configure<ApiEndpointsConfiguration>(
    builder.Configuration.GetSection("ApiEndpoints"));

builder.Services.AddHttpClient<IHttpClientHelper, HttpClientHelper>(client => 
{
    client.BaseAddress = new Uri(apiBaseAddress);
});

builder.Services.AddPersonsServices();
builder.Services.AddAccountsServices();
builder.Services.AddTransactionsServices();
builder.Services.AddScoped<Stopwatch>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
