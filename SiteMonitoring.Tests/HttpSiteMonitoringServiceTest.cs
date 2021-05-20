using System;
using System.Net.Http;
using SiteMonitoring.Core.Entities.Models;
using SiteMonitoring.Core.Monitoring.Services;
using Xunit;

namespace SiteMonitoring.Tests
{
    public class HttpSiteMonitoringServiceTest
    {
        [Fact]
        public async void YandexStatus()
        {
            var httpClient = new HttpClient();
            var service = new HttpSiteMonitoringService(httpClient);

            var siteInfo = new SiteInfo()
            {
                Name = "yandex",
                RefreshInterval = 60000,
                Url = "https://yandex.ru"
            };
            var status = await service.GetResponseAsync(siteInfo);

            Assert.Equal(200, status.StatusCode);
        }
    }
}
