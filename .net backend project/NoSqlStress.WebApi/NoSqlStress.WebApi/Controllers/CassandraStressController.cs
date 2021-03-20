using System.Threading;
using System.Threading.Tasks;
using Cassandra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoSqlStress.WebApi.Dto;

namespace NoSqlStress.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CassandraStressController : ControllerBase
    {
        private const string KEY_SPACE = "test";
        private const int REPLICA_COUNT = 3;

        private readonly ILogger<CassandraStressController> _logger;
        private readonly ICluster _cassandraCluster;

        public CassandraStressController(ILogger<CassandraStressController> logger, ICluster cassandraCluster)
        {
            _logger = logger;
            _cassandraCluster = cassandraCluster;
        }

        [HttpPost(nameof(CreateSchema))]
        public async Task<IActionResult> CreateSchema()
        {
            using (var context = await _cassandraCluster.ConnectAsync())
            {
                context.CreateKeyspaceIfNotExists(KEY_SPACE, ReplicationStrategies.CreateSimpleStrategyReplicationProperty(REPLICA_COUNT));
            }

            using (var context = await _cassandraCluster.ConnectAsync(KEY_SPACE))
            {
                await context.ExecuteAsync(new SimpleStatement("CREATE TABLE IF NOT EXISTS test_table (id text PRIMARY KEY, entity text);"));
            }

            return Ok();
        }

        [HttpPost(nameof(WriteEntity))]
        public async Task<IActionResult> WriteEntity(Entity entity)
        {
            using (var context = await _cassandraCluster.ConnectAsync(KEY_SPACE))
            {
                var preparedStatement = context.Prepare("INSERT INTO test_table(id, entity) VALUES (?,?)");
                var statement = preparedStatement.Bind(entity.Id, entity.Json);

                await context.ExecuteAsync(statement);
            }

            return Ok();
        }
    }
}
