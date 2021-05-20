using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SiteMonitoring.Core.Entities.Models;
using SiteMonitoring.Core.Monitoring;
using SiteMonitoring.Core.Monitoring.Interfaces;
using SiteMonitoring.Core.Monitoring.Services;
using SiteMonitoring.Core.Monitoring.Services.Interfaces;
using Xunit;

namespace SiteMonitoring.Tests
{
    public class MonitoringTest
    {
        [Fact(DisplayName = "TestMonitoringServices")]
        public void TestSiteMonitoring()
        {
            var monitoringService = new SiteMonitoringServiceStub();
            var dataContext = new DataContextStub();

            var monitoringManager = new SiteMonitoringManager(monitoringService, dataContext);

            Thread.Sleep(90000);

            Assert.True(dataContext.GetResponses().Any());
        }

        [Fact(DisplayName = "TestHttpMonitoringService")]
        public void TestHttpSiteMonitoringService()
        {
            var httpClient = new HttpClient();

            var monitoringService = new HttpSiteMonitoringService(new HttpClient());
            var dataContext = new DataContextStub();

            var monitoringManager = new SiteMonitoringManager(monitoringService, dataContext);

            Thread.Sleep(90000);

            Assert.True(dataContext.GetResponses().Any());
        }
    }

    public class DataContextStub: IMonitoringDataContext
    {
        private List<SiteResponse> responses = new();
        private List<SiteInfo> sites = new List<SiteInfo>()
        {
            new SiteInfo()
            {
                Name = "yandex",
                Url = "https://yandex.ru",
                RefreshInterval = 60000
            }
        };

        public IEnumerable<SiteInfo> GetSites() => sites;

        public List<SiteResponse> GetResponses() => responses;

        public async Task SaveSiteStatusAsync(SiteResponse response)
        {
            responses.Add(response);

            await Task.FromResult<IEnumerable<SiteResponse>>(responses);
        }
    }

    public class SiteMonitoringServiceStub : ISiteMonitorService
    {
        private SiteResponse response = new SiteResponse()
        {
            StatusCode = 200,
            DateTime = DateTime.Now,
            SiteName = "yandex"
        };

        public async Task<SiteResponse> GetResponseAsync(SiteInfo siteInfo)
        {
            return await Task.FromResult<SiteResponse>(response);
        }
    }
}
