using FlightSearch.Data;
using FlightSearch.External;
using FlightSearch.Server.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFlightSearchExternal(builder.Configuration);
builder.Services.AddFlightSearchData(builder.Configuration);

builder.Services.AddTransient<IFlightSearchService, FlightSearchService>();
builder.Services.AddLazyCache();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await new DbInitializer(scope).InitAsync();
    new PreloadDataFromDatabase(scope).PreloadResponsesFromDbToCache(); 
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();




