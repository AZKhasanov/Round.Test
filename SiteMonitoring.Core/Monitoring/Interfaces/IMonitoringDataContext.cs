using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteMonitoring.Core.Entities.Models;

namespace SiteMonitoring.Core.Monitoring.Interfaces
{
    public interface IMonitoringDataContext
    {
        IEnumerable<SiteInfo> GetSites();
        Task SaveSiteStatusAsync(SiteResponse response);
    }
}
