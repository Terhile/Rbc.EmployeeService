using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Rbc.EmployeeService.Services.Logger
{
    public class ActivityLogger : IActivityLogger
    {
        private readonly ILogger _logger;

        public ActivityLogger(ILogger<ActivityLogger> logger)
        {
            _logger = logger;
        }
        public void LogRequestResponse(string response)
        {

            _logger.LogError(response.ToString());
        }
    }
}
