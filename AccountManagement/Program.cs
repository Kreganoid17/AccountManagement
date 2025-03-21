using AccountManagement.ActionFilter;
using AccountManagement.Configuration;
using AccountManagement.Domains.Accounts;
using AccountManagement.Domains.Persons;
using AccountManagement.Domains.Transactions;
using HttpClientLibrary.HttpClientService;
using HttpClientLibrary.Interface;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Enrichers.Span;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var baseUrl = builder.Configuration["HttpClientApiUris:AccountManagementAPIAddress"] ?? throw new InvalidOperationException("The API configuration key is missing: AccountManagementAPIAddress");

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

builder.Services.Configure<ConnectionStringOptions>(
    builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.Configure<StoredProcedureOptions>(
    builder.Configuration.GetSection("StoredProcedures"));

builder.Services.AddHttpClient(AccountManagement.Helpers.Constants.httpClientName, client =>
{
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped(typeof(IHttpClientHelper<>), typeof(HttpClientHelper<>));

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
