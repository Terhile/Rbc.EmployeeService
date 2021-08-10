using System.Net;

namespace Rbc.EmployeeService.Services.Logger
{
    public interface IActivityLogger
    {
        void LogRequestResponse( string message);
    }
}
