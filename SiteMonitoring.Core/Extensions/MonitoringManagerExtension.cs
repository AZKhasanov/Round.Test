using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SiteMonitoring.Core.Monitoring.Extensions
{
    public static class MonitoringManagerExtension
    {
        public static IServiceCollection AddSiteMonitoringManager(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SiteMonitoringManager>();
            var provider = serviceCollection.BuildServiceProvider();
            provider.GetService<SiteMonitoringManager>();
            return serviceCollection;
        }
    }
}
