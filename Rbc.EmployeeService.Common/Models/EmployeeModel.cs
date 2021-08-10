using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbc.EmployeeService.Common.Models
{
   public class EmployeeModel
    {
        [JsonRequired]
        public string EmployeeNumber { get; set; }

        [JsonRequired]
        public string EmployeeName { get; set; }

        [JsonRequired]
        public decimal HourlyRate { get; set; }
        [JsonRequired]
        public int HoursWorked { get; set; }

        [JsonRequired]
        public decimal TotalPay { get; set; }
    }
}
