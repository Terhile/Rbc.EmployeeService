using System.Threading.Tasks;
using Confluent.Kafka;
using Rbc.EmployeeService.Common.Models;

namespace Rbc.EmployeeService.Services.Employee
{
    public interface IEmployeeKafkaService
    {

        public void Produce(EmployeeModel employee);
        public Task<EmployeeModel> Consume();
        void deliveryReportHandler(DeliveryReport<string, string> deliveryReport);
    }
}
