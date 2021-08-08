using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbc.EmployeeService.Common.Configurations
{
   public class KafkaEmployerSettings
    {
        public string BootstrapServers { get; set; }
        public string GroupId { get; set; }
        public string SaslMechanism { get; set; }
        public string TopicName { get; set; }

        public string SecurityProtocol { get; set; }
    }
}
