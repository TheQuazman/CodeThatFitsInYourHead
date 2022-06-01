
using Restaurant.RestApi;
using Restaurant.RestApi.Settings;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connStr = builder.Configuration.GetConnectionString("Restaurant");
builder.Services.AddSingleton<IReservationsRepository>(new SqlReservationsRepository(connStr));

var settings = new RestaurantSettings();
builder.Configuration.Bind("Restaurant", settings);
builder.Services.AddSingleton(settings.ToMaitreD());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

public partial class Program { }