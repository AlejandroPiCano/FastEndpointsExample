using FastEndPointsExample.DTOs;
using FastEndPointsExample.Redis;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace FastEndPointsExample.EndPoints
{
    public class FastSingleGetByIdRequest : Endpoint<ResponseGUID>
    {
        public IDatabase DbContext { get; set; }

        public FastSingleGetByIdRequest(IDatabase dbContext)
        {
            DbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/api/getResponseById");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ResponseGUID input, CancellationToken ct)
        {
            string responseStr = DbContext.StringGet(input.GUID);

            var response = JsonConvert.DeserializeObject<ResponseDTO>(responseStr);

            await SendAsync(response);
        }
    }
}
