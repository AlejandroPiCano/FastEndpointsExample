using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FastEndPointsExample.Redis
{
    internal static class RedisDB
    {       
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            var config = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                AbortOnConnectFail = false, //Setting AbortOnConnectFail to false will tell StackExchange.Redis to automatically reconnect in the background when the connection is lost for any reason
                ConnectRetry = 10,
                ReconnectRetryPolicy = new ExponentialRetry(5000),
                ClientName = "ApiClient",
                AllowAdmin = true
            };

            return ConnectionMultiplexer.Connect(config);
        });
    }
}
