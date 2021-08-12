using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rbc.EmployeeService.Common.Configurations;
using Rbc.EmployeeService.Common.Models;
using Rbc.EmployeeService.Services.Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rbc.EmployeeService.Services.Employee
{
    public class EmployeeBackgroundService : BackgroundService
    {
        private readonly IActivityLogger _logger;
        private readonly KafkaEmployerSettings _kafkaEmployeeSettings;
        private readonly IConsumer<string, string> kafkaConsumer;

        public EmployeeBackgroundService( IActivityLogger logger, IOptions<KafkaEmployerSettings> producerSettings)
        {

            _logger = logger;
            _kafkaEmployeeSettings = producerSettings.Value;
            var conf = new ConsumerConfig
            {

                BootstrapServers = _kafkaEmployeeSettings.BootstrapServers,
                GroupId = _kafkaEmployeeSettings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                EnableAutoOffsetStore = true,
            };
            this.kafkaConsumer = new ConsumerBuilder<string, string>(conf).Build();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            new Thread(() => ReadMessages(stoppingToken)).Start();

            return Task.CompletedTask;
        }

        private void ReadMessages(CancellationToken cancellationToken)
        {
            kafkaConsumer.Subscribe(this._kafkaEmployeeSettings.TopicName);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var cr = this.kafkaConsumer.Consume(cancellationToken);

                    EmployeeModel employee = JsonConvert.DeserializeObject<EmployeeModel>(cr.Message?.Value);

                    _logger.LogRequestResponse($"Employee with ID '{employee.EmployeeNumber}' read from '{cr.Topic}' at timestamp' {cr.Message.Timestamp.UtcDateTime}'");

                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    _logger.LogRequestResponse($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogRequestResponse($"Unexpected error: {e}");
                    break;
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogRequestResponse(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
