using System;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoSqlStress.WebApi.Dto;

namespace NoSqlStress.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CassandraStressController : ControllerBase, IStressController
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
                await context.ExecuteAsync(new SimpleStatement($@"
CREATE TABLE IF NOT EXISTS {nameof(TestEntity)} (
id uuid PRIMARY KEY, 
name text,
age int,
description text,
address text,
phone text,
email text);"));
            }

            return Ok();
        }

        /// <inheritdoc />
        [HttpPost(nameof(WriteEntity))]
        public async Task<IActionResult> WriteEntity(TestEntity entity)
        {
            using (var context = await _cassandraCluster.ConnectAsync(KEY_SPACE))
            {
                var mapper = new Mapper(context);

                await mapper.InsertAsync(entity);
                
                //var preparedStatement = await context.PrepareAsync("INSERT INTO test_table(id, name) VALUES (?,?)");
                //var statement = preparedStatement.Bind(entity.Id, entity.Name);
                //await context.ExecuteAsync(statement);
            }

            return Ok();
        }

        /// <inheritdoc />
        [HttpGet(nameof(ReadEntity))]
        public async Task<IActionResult> ReadEntity(Guid id)
        {
            using (var context = await _cassandraCluster.ConnectAsync(KEY_SPACE))
            {
                var mapper = new Mapper(context);

                //var preparedStatement = await context.PrepareAsync("SELECT json FROM test_table WHERE id = ?");
                //var statement = preparedStatement.Bind(id);

                //var row = await context.ExecuteAsync(statement);
                //var json = row.FirstOrDefault()?[0];
                
                var entity = await mapper.FirstOrDefaultAsync<TestEntity>($"FROM {nameof(TestEntity)} WHERE id = ?", id);

                return Ok(entity);
            }
        }
    }
}
