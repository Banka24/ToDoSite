using StackExchange.Redis;

namespace ToDoAPI.DataAccess
{
    public class ToDoRedisDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionMultiplexer _redis;

        public IDatabase Redis { get; private set; }

        public ToDoRedisDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _redis = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis")!);
            Redis = _redis.GetDatabase();
        }
    }
}
