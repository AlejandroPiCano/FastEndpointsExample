using FastEndPointsExample.DTOs;
using FastEndPointsExample.Redis;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace FastEndPointsExample.EndPoints
{
    public class FastSingleGetAllResponsesdRequest : EndpointWithoutRequest<List<ResponseDTO>>
    {
        public IDatabase DbContext { get; set; }

        public FastSingleGetAllResponsesdRequest(IDatabase dbContext)
        {
            DbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/api/getAllResponses");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            EndPoint endPoint = RedisDB.Connection.GetEndPoints().First();
            RedisKey[] keys = RedisDB.Connection.GetServer(endPoint).Keys(pattern: "*").ToArray();

            List<ResponseDTO> result = new List<ResponseDTO>();

            foreach (var key in keys)
            {
                string responseStr = DbContext.StringGet(key);

                result.Add(JsonConvert.DeserializeObject<ResponseDTO>(responseStr));
            }
           
            await SendAsync(result);
        }
    }
}
