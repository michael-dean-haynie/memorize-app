using System;
using System.Collections.Generic;
using System.Diagnostics;
using Azure;
using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace memorize_app
{
    public class MemorizeApp
    {
        [FunctionName("MemorizeApp")]
        public void Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            string now = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            
            // check if there are any memorization items ready for a prompt
            // load the item that is most overdue for a prompt
            var storageAccountConn = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            var tableClient = new TableClient(storageAccountConn, "MemorizationItem");

            string filter = $"NextPromptTimestamp lt datetime'{now}'";
            var pageable = tableClient.Query<MemorizationItem>(filter, 5);

            var items = new Dictionary<string, MemorizationItem>();
            foreach (var item in pageable)
            {
                items[item.RowKey] = item;
            }
            

            // send the prompt

        }
    }
}
