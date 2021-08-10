using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rbc.EmployeeService.Common.Models
{
  public  class ResponseModel
    {
        public bool Success { get; set; } = false;

        public string Message { get; set; }

        public int ResponseCode { get; set; }

    }
}
