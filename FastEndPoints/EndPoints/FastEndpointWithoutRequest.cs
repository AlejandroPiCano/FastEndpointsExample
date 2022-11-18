using FastEndPointsExample.DTOs;
using FastEndPointsExample.Redis;
using StackExchange.Redis;
using System.Net;
using System.Reflection.Metadata;

namespace FastEndPointsExample.EndPoints
{

    public class FastEndpointWithoutRequest : EndpointWithoutRequest<List<string>>
    {
        public IDatabase DbContext { get; }

        public FastEndpointWithoutRequest(IDatabase dbContext)
        {
            DbContext = dbContext;
        } 

        public override void Configure()
        {
            Get("/api/keys");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            EndPoint endPoint = RedisDB.Connection.GetEndPoints().First();
            RedisKey[] keys = RedisDB.Connection.GetServer(endPoint).Keys(pattern: "*").ToArray();

            Response = keys.Select(rk => rk.ToString()).ToList();

        }
    }
}
