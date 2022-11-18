using FastEndPointsExample.DTOs;
using FastEndPointsExample.Redis;
using StackExchange.Redis;
using System.Net;
using System.Reflection.Metadata;

namespace FastEndPointsExample.EndPoints
{

    public class FastEndpointDeleteAllKeysWithoutRequest : EndpointWithoutRequest<List<string>>
    {
        public IDatabase DbContext { get; }

        public FastEndpointDeleteAllKeysWithoutRequest(IDatabase dbContext)
        {
            DbContext = dbContext;
        } 

        public override void Configure()
        {
            Get("/api/DeleteAllkeys");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            EndPoint endPoint = RedisDB.Connection.GetEndPoints().First();
            var server = RedisDB.Connection.GetServer(endPoint);

            server.FlushDatabaseAsync();

        }
    }
}
