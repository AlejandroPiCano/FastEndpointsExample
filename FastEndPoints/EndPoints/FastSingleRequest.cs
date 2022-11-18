using FastEndPointsExample.DTOs;
using FastEndPointsExample.Redis;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace FastEndPointsExample.EndPoints
{
    public class FastSingleRequest : Endpoint<RequestDTO>
    {
        public IDatabase DbContext { get; set; }

        public FastSingleRequest(IDatabase dbContext)
        {
            DbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/api/createResponse");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestDTO req, CancellationToken ct)
        {
            var response = new ResponseDTO()
            {
                FullName = req.FirstName + " " + req.LastName,
                IsOver18 = req.Age > 18
            };
            
            DbContext.StringSet(Guid.NewGuid().ToString(), JsonConvert.SerializeObject(response));

            await SendAsync(response);
        }
    }
}
