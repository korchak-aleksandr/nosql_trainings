using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NoSqlStress.WebApi.Dto;

namespace NoSqlStress.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MongoStressController : ControllerBase, IStressController
    {
        private const string DATABASE = "test";
        private const string COLLECTION = "tests";

        private readonly ILogger<MongoStressController> _logger;
        private readonly MongoClientBase _mongoClient;
        private readonly MongoCollectionSettings _defaultCollectionSettings;

        public MongoStressController(ILogger<MongoStressController> logger, MongoClientBase mongoClient)
        {
            _logger = logger;
            _mongoClient = mongoClient;
            _defaultCollectionSettings = new MongoCollectionSettings
            {
                WriteConcern = WriteConcern.W1,
                ReadConcern = ReadConcern.Local
            };
        }

        /// <inheritdoc />
        [HttpPost(nameof(WriteEntity))]
        public async Task<IActionResult> WriteEntity(TestEntity entity)
        {
            var database = _mongoClient.GetDatabase(DATABASE);
            var collection = database.GetCollection<TestEntity>(COLLECTION, _defaultCollectionSettings);

            await collection.InsertOneAsync(entity);
            
            return Ok();
        }

        /// <inheritdoc />
        [HttpGet(nameof(ReadEntity))]
        public async Task<IActionResult> ReadEntity(Guid id)
        {
            var database = _mongoClient.GetDatabase(DATABASE);
            var collection = database.GetCollection<TestEntity>(COLLECTION, _defaultCollectionSettings);

            var entity = await collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            return Ok(entity);
        }
    }
}
