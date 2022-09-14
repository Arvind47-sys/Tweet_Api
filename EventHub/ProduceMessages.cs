using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using log4net;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;
using Tweet_Api.Logger;

namespace Tweet_Api.EventHub
{
    public class ProduceMessages : IProduceMessages
    {
        private EventHubProducerClient _producer;
        private ILog logger;

        public ProduceMessages(IConfigurationSection eventHubConfig)
        {
            _producer = new EventHubProducerClient(eventHubConfig["ConnectionString"], eventHubConfig["EventHubName"]);
            logger = LogManager.GetLogger(typeof(LogFilterAttribute));
        }

        public async Task ProduceAsync(string value)
        {
            try
            {
                using EventDataBatch eventBatch = await _producer.CreateBatchAsync();
                if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event {value}"))))
                {
                    throw new Exception($"Event {value} is too large for the batch and cannot be sent.");
                }
                await _producer.SendAsync(eventBatch);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + " - " + ex.StackTrace);
            }
        }

        public async Task Dispose()
        {
            await _producer.DisposeAsync();
        }
    }
}