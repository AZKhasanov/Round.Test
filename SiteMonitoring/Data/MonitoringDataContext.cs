using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiteMonitoring.Core.Entities.Models;
using SiteMonitoring.Core.Monitoring.Interfaces;

namespace SiteMonitoring.Data
{
    public class MonitoringDataContext: IMonitoringDataContext
    {
        private IServiceProvider _provider;
        private DataContext DBContext
        {
            get => (DataContext) _provider.GetService(typeof(DataContext));
        }

        public MonitoringDataContext(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<SiteInfo> GetSites()
        {
            return DBContext.Sites.Select(s => new SiteInfo()
            {
                Name = s.Name,
                Url = s.Url,
                RefreshInterval = s.RefreshInterval
            }).ToList();
        }

        public async Task SaveSiteStatusAsync(SiteResponse response)
        {
            using (var dbContext = DBContext)
            {
                var site = dbContext.Sites.Where(s => s.Name == response.SiteName).SingleOrDefault();
                if (site != null)
                {
                    await dbContext.SiteStatuses.AddAsync(new SiteStatus()
                    {
                        SiteId = site.Id,
                        StatusCode = response.StatusCode,
                        UpdateDateTime = response.DateTime,
                        Message = response.Message
                    });

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
