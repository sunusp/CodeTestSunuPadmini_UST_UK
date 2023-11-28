using MessagingService.Models;
using Microsoft.Extensions.Configuration;
using messagingService = MessagingService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.Configure<SMTPClientSettings>(builder.Configuration.GetSection("SMTPClientSettings"));
builder.Services.AddScoped<messagingService.IMessagingService, messagingService.MessagingService>();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
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
