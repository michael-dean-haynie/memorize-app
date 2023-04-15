using System;
using System.Linq;
using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;

namespace memorize_app
{
    public class MemorizeApp
    {
        [FunctionName("MemorizeApp")]
        public void Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            DateTime now = DateTime.UtcNow;
            string tableName = "MemorizationItem";
            string storageAccountConn = Environment.GetEnvironmentVariable("AzureWebJobsStorage")
                ?? throw new Exception("AzureWebJobsStorage environment variable should not be null.");

            var tableClient = new TableClient(storageAccountConn, tableName);
            var query = tableClient.Query<MemorizationItem>()
                .Where(mi => mi.NextPromptTimestamp <= now)
                .Where(mi => mi.Term == "leonine");

            var result = query.ToList();

            int foo = 1;

        }
    }
}
