using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Rbc.EmployeeService.Common.Models;

namespace Rbc.EmployeeService.Services.Employee
{
    public interface IEmployeeKafkaService
    {

        public void WriteMessage(EmployeeModel employee);
        void deliveryReportHandler(DeliveryReport<string, string> deliveryReport);
    }
}
