using System;
using System.Collections.Generic;
using Azure;
using Azure.Data.Tables;

namespace memorize_app;

public class MemorizationItem : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    
    public DateTimeOffset CreatedTimestamp { get; set; }
    public DateTimeOffset NextPromptTimestamp { get; set; }
}