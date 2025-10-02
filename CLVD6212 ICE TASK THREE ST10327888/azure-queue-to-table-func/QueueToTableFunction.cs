using System;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace QueueToTableFunction
{
    public class QueueMessageEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "QueuePartition";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string MessageText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Azure.ETag ETag { get; set; } = Azure.ETag.All;
        public DateTimeOffset? Timestamp { get; set; }
    }

    public class QueueToTable
    {
        private readonly ILogger _logger;
        private readonly TableClient _tableClient;

        public QueueToTable(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<QueueToTable>();
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            _tableClient = new TableClient(connectionString, "QueueMessages");
            _tableClient.CreateIfNotExists();
        }

        [Function("QueueToTable")]
        public async Task Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")] string myQueueItem)
        {
            _logger.LogInformation($"Processing queue item: {myQueueItem}");

            var entity = new QueueMessageEntity
            {
                MessageText = myQueueItem
            };

            await _tableClient.AddEntityAsync(entity);
            _logger.LogInformation("Inserted into Table Storage successfully.");
        }
    }
}
