using System.Text.Json.Serialization;
using L3WebAPI.Business.Implementations;
using L3WebAPI.Business.Interfaces;
using L3WebAPI.DataAccess.Implementations;
using L3WebAPI.DataAccess.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IGamesService, GamesService>();
builder.Services.AddTransient<IGamesDataAccess, GamesDataAccess>();

builder.Services.AddControllers().AddJsonOptions(options => {
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerUI(options => {
	options.ConfigObject.Urls = [new UrlDescriptor {
		Name = "L3 Web API",
		Url = "/openapi/v1.json"
	}];
});

app.Run();

public partial class Program { }
