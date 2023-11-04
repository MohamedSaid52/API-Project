using API.BLL.Interfaces___Copy;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.DAL.Services
{
    public class ResponseCashService : IResponseCashService
    {
        private readonly IDatabase _database;
        private readonly ILogger<ResponseCashService> logger;

        public ResponseCashService(IConnectionMultiplexer redis,ILogger<ResponseCashService> logger)
        {
            _database = redis.GetDatabase();
            this.logger = logger;
        }
        public async Task CashResponseAsync(string CashKey, object Response, TimeSpan timetolive)
        {
            if (Response is null) return;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var SerializedResponse = JsonSerializer.Serialize(Response, options);
            await _database.StringSetAsync(CashKey, SerializedResponse, timetolive);
        }

        public async Task<string> GetCashResponse(string CashKey)
        {
            var cashResponse= await _database.StringGetAsync(CashKey);
            logger.LogInformation("Cash Response");
            if (cashResponse.IsNullOrEmpty) return null;
            return cashResponse.ToString();
        }
    }
}
