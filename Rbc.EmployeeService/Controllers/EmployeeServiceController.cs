using Microsoft.AspNetCore.Mvc;
using Rbc.EmployeeService.Common.Models;
using System.Threading.Tasks;
using Rbc.EmployeeService.Services.Employee;
using Rbc.EmployeeService.Services.Logger;

namespace Rbc.EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeServiceController : ControllerBase
    {
        private readonly IEmployeeKafkaService _employeeService;
        private readonly IActivityLogger _logger;
        public EmployeeServiceController(IEmployeeKafkaService employeeKafkaService, IActivityLogger logger)
        {
            _employeeService = employeeKafkaService;
            _logger = logger;

        }

        [HttpPost("publish")]
        public async  Task<IActionResult> PublishEmployeeInfo(EmployeeModel model)
        {
            _logger.LogRequestResponse("request recived");
              _employeeService.WriteMessage(model);

            return Ok();
            
        }
    }
}
