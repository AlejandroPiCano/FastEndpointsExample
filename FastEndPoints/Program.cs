global using FastEndpoints;
using FastEndPointsExample.Redis;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.AddSingleton(provider => RedisDB.Connection.GetDatabase());

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();
