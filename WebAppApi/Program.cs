using WebAppApi.Filters;
using WebAppApi.Middlewares;
using WebAppApi.Services;

var builder = WebApplication.CreateBuilder(args);
var AppName = builder.Configuration["AppName"];
var Language = builder.Configuration["Language"];
var Country = builder.Configuration["Country"];
var Log = builder.Configuration["Logging:LogLevel:Default"];

Console.WriteLine("AppName=" +  AppName + " - Language=" + Language + " - Country=" + Country + " - Log=" + Log );
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TimeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
// app.UseMiddleware<StatsMiddleware>();

/*
// executed each time send request to server. inline middleware, calculate execution time of the request
app.Use((context, next) =>
{
	DateTime requestTime = DateTime.Now;

	var result =  next(context); // everything before this statement will get executed before controller's execution

	DateTime responseTime = DateTime.Now;
	TimeSpan processDuration = responseTime - requestTime;
	Console.WriteLine("[Inline Middleware] Process Duration=" + processDuration.TotalMilliseconds + "ms");
	return result;

}); 
*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
