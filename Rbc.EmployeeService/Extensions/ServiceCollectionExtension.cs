using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rbc.EmployeeService.Common.Configurations;
using Rbc.EmployeeService.Services.Employee;
using Rbc.EmployeeService.Services.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rbc.EmployeeService.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<KafkaEmployerSettings>(config.GetSection("Kafka:EmployerSettings"));
            services.AddSingleton<ProducerWrapper>();
            services.AddSingleton<IEmployeeKafkaService, EmployeeKafkaService>();
            services.AddSingleton<IActivityLogger, ActivityLogger>();
            services.AddHostedService<EmployeeBackgroundService>();

            return services;
        }
    }
}
