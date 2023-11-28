using ServiceClient.httpClients;
using customerService = CustomerService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddScoped<customerService.ICustomerService, customerService.CustomerService>();

var messagingAPIBaseAddress = builder.Configuration.GetValue<string>("MessagingAPIBaseAddress");
builder.Services.AddHttpClient<IMessagingClient, MessagingClient>(client =>
{
    client.BaseAddress = new Uri(messagingAPIBaseAddress);
});
builder.Services.AddSwaggerGen();

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
