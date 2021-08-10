using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Rbc.EmployeeService.Common.Configurations;
using System;

namespace RbC.EmployeeService.KafkaService.Producer
{
    public class ProducerClientHandle : IDisposable
    {
        private readonly IProducer<string, string> _kafkaProducer;
        private readonly KafkaEmployerSettings _kafkaEmployeeSettings;
        public ProducerClientHandle( IOptions<KafkaEmployerSettings> producerSettings)
        {
            _kafkaEmployeeSettings = producerSettings.Value;
            var conf = new ProducerConfig { BootstrapServers = _kafkaEmployeeSettings .BootstrapServers};
            _kafkaProducer = new ProducerBuilder<string, string>(conf).Build();
        }
        public void Dispose()
        {
            _kafkaProducer.Flush();
            _kafkaProducer.Dispose();
        }

        public Handle Handle { get => this._kafkaProducer.Handle; }

    }
}
