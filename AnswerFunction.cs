using System;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace memorize_app
{
    public class AnswerFunction
    {
        [FunctionName("AnswerFunction")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "answer")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
        }
    }
}
