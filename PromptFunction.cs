using System;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;
using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace memorize_app
{
    public class PromptFunction
    {
        [FunctionName("PromptFunction")]
        public  async Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            DateTime now = DateTime.UtcNow;
            string tableName = "MemorizeApp";
            string storageAccountConn = Environment.GetEnvironmentVariable("AzureWebJobsStorage")
                ?? throw new Exception("AzureWebJobsStorage environment variable should not be null.");

            var tableClient = new TableClient(storageAccountConn, tableName);
            var query = tableClient.Query<TermEntity>()
                .Where(mi => mi.PartitionKey == "term")
                .Where(mi => mi.NextPromptTimestamp <= now)
                .Take(1);

            var result = query.ToList();


            string comSvcConn = Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING")
                ?? throw new Exception("COMMUNICATION_SERVICES_CONNECTION_STRING environment variable should not be null.");
            var emailClient = new EmailClient(comSvcConn);

            var subject = "Welcome to Azure Communication Service Email APIs.";
            var htmlContent = "<html><body><h1>Quick send email test</h1><br/><h4>This email message is sent from Azure Communication Service Email.</h4><p>This mail was send using .NET SDK!!</p></body></html>";
            var sender = "donotreply@2f84d7d2-4cea-4abc-a1e8-be546fa71d5f.azurecomm.net";
            var recipient = "michael.dean.haynie@gmail.com";

            try
            {
                EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                    Azure.WaitUntil.Completed,
                    sender,
                    recipient,
                    subject,
                    htmlContent);
                EmailSendResult statusMonitor = emailSendOperation.Value;

                // Get the OperationId so that it can be used for tracking the message for troubleshooting
                string operationId = emailSendOperation.Id;
            }
            catch (RequestFailedException ex)
            {
                // OperationID is contained in the exception message and can be used for troubleshooting purposes
                Console.WriteLine(
                    $"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");
            }

            int foo = 1;

        }
    }
}
