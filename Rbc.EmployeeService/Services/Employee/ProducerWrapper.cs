using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Rbc.EmployeeService.Common.Configurations;
using System;

namespace Rbc.EmployeeService.Services.Employee
{
    public class ProducerWrapper : IDisposable
    {
        private readonly IProducer<string, string> _kafkaProducer;
        private readonly KafkaEmployerSettings _kafkaEmployeeSettings;
        public ProducerWrapper(IOptions<KafkaEmployerSettings> producerSettings)
        {
            _kafkaEmployeeSettings = producerSettings.Value;
            var conf = new ProducerConfig { BootstrapServers = _kafkaEmployeeSettings.BootstrapServers, };
            this._kafkaProducer = new ProducerBuilder<string, string>(conf).Build();
        }
        public void Dispose()
        {
            _kafkaProducer.Flush();
            _kafkaProducer.Dispose();
        }

        public Handle Handle { get => _kafkaProducer.Handle; }

    }
}
