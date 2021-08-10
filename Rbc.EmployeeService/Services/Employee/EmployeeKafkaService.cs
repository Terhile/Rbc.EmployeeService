using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rbc.EmployeeService.Common.Configurations;
using Rbc.EmployeeService.Common.Models;
using Rbc.EmployeeService.Services.Logger;
using RbC.EmployeeService.KafkaService.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rbc.EmployeeService.Services.Employee
{
    public class EmployeeKafkaService : IEmployeeKafkaService
    {
        private readonly ProducerClientHandle _producerClient;
        IProducer<string, string> producer;
        private readonly KafkaEmployerSettings _kafkaEmployeeSettings;
        private readonly IActivityLogger _logger;

        public EmployeeKafkaService(ProducerClientHandle producerClientHandle, IOptions<KafkaEmployerSettings> producerSettings, IActivityLogger logger)
        {
            _producerClient = producerClientHandle;
            producer = new DependentProducerBuilder<string, string>(_producerClient.Handle).Build();
            _kafkaEmployeeSettings = producerSettings.Value;
            _logger = logger;
        }
        public Task<EmployeeModel> Consume()
        {
            throw new NotImplementedException();
        }

        public void deliveryReportHandler(DeliveryReport<string, string> deliveryReport)
        {
            if (deliveryReport.Status == PersistenceStatus.NotPersisted)
            {
                _logger.LogRequestResponse( $"Failed to log request time for path: {deliveryReport.Message.Key}");
            }
        }

        public void Produce(EmployeeModel employee)
        {
 
            var empInfo = JsonConvert.SerializeObject(employee);  //should use schema registry here to handle  serialization/deserialization of data model  
            producer.Produce(_kafkaEmployeeSettings.TopicName, new Message<string, string> {Key = employee.EmployeeNumber, Value = empInfo }, deliveryReportHandler);
        }
    }
}
