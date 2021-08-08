using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rbc.EmployeeService.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rbc.EmployeeService.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ApplicationServices(IServiceCollection services, IConfiguration config)
        {

            services.Configure<KafkaEmployerSettings>(config.GetSection("Kafka:EmployerSettings"));

            return services;
        }
    }
}
