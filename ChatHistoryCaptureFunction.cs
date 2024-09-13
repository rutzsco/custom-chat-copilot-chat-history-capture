using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ChatHistoryCaptureProcessor
{
    public class ChatHistoryCaptureFunction
    {
        private readonly ILogger _logger;

        public ChatHistoryCaptureFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ChatHistoryCaptureFunction>();
        }

        [Function("ChatHistoryCaptureFunction")]
        [EventHubOutput("%EventHubName%", Connection = "EventHubConnection")]
        public List<EventData> Run([CosmosDBTrigger(databaseName: "ChatHistory",containerName: "ChatTurn", Connection = "CosmosDBConnectionString", LeaseContainerName = "leases", CreateLeaseContainerIfNotExists = true)] IReadOnlyList<JsonDocument> documents)
        {
            var events = new List<EventData>();
            if (documents != null && documents.Count > 0)
            {
                _logger.LogInformation("Documents modified: " + documents.Count);

                foreach (var document in documents)
                {
                    string jsonString = document.RootElement.ToString();
                    var eventData = new EventData(Encoding.UTF8.GetBytes(jsonString));
                    events.Add(eventData);
                }
            }
            return events;
        }
    }
}