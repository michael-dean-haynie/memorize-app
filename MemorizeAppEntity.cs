using System;
using Azure;
using Azure.Data.Tables;

namespace memorize_app
{
    public class MemorizeAppEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}