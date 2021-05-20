using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteMonitoring.Core.Entities.Models;

namespace SiteMonitoring.Core.Monitoring.Services.Interfaces
{
    public interface ISiteMonitorService
    {
        Task<SiteResponse> GetResponseAsync(SiteInfo siteInfo);
    }
}
