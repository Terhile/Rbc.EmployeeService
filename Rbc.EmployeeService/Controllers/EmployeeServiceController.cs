using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Rbc.EmployeeService.Common.Models;
using System.Threading.Tasks;
using Rbc.EmployeeService.Services.Employee;

namespace Rbc.EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeServiceController : ControllerBase
    {
        private readonly IEmployeeKafkaService _employeeService;
        
        public EmployeeServiceController(IEmployeeKafkaService employeeKafkaService)
        {
            _employeeService = employeeKafkaService;

        }

        [HttpPost("publish")]
        public async  Task<IActionResult> PublishEmployeeInfo(EmployeeModel model)
        {
              _employeeService.Produce(model);

            return Ok();
            
        }
    }
}
