using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rbc.EmployeeService.Common.Configurations;
using Rbc.EmployeeService.Common.Models;
using Rbc.EmployeeService.Services.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rbc.EmployeeService.Services.Employee
{
    public class EmployeeKafkaService : IEmployeeKafkaService
    {

        private readonly IProducer<string, string> producer;
        private readonly KafkaEmployerSettings _kafkaEmployeeSettings;
        private readonly IActivityLogger _logger;

        public EmployeeKafkaService(ProducerWrapper producerClientHandle, IOptions<KafkaEmployerSettings> producerSettings, IActivityLogger logger)
        {
            producer = new DependentProducerBuilder<string, string>(producerClientHandle.Handle).Build();
            _kafkaEmployeeSettings = producerSettings.Value;
            _logger = logger;
        }
        

        public void deliveryReportHandler(DeliveryReport<string, string> deliveryReport)
        {
            if (deliveryReport.Status == PersistenceStatus.NotPersisted)
                _logger.LogRequestResponse($"Failed to log request time for path: {deliveryReport.Message.Key}");
            else
                _logger.LogRequestResponse($"employee info successfullly publish to topic {_kafkaEmployeeSettings.TopicName},  Employee key: {deliveryReport.Message.Key}");
        }

        public void Flush(TimeSpan timeout)
            => this.producer.Flush(timeout);
        public void WriteMessage(EmployeeModel employee)
        {

            var empInfo = JsonConvert.SerializeObject(employee);  //should use schema registry here to handle schema validation or  serialization/deserialization of data model  
            producer.Produce(_kafkaEmployeeSettings.TopicName, new Message<string, string> { Key = employee.EmployeeNumber, Value = empInfo }, deliveryReportHandler);
        }
    }
}
