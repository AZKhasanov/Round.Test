using System;
using System.Net.Http;
using System.Threading.Tasks;
using SiteMonitoring.Core.Entities.Models;
using SiteMonitoring.Core.Monitoring.Services.Interfaces;

namespace SiteMonitoring.Core.Monitoring.Services
{
    public class HttpSiteMonitoringService: ISiteMonitorService
    {
        private HttpClient _httpClient;

        public HttpSiteMonitoringService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SiteResponse> GetResponseAsync(SiteInfo siteInfo)
        {
            try
            {
                var response = await _httpClient.GetAsync(siteInfo.Url);

                return new SiteResponse()
                {
                    SiteName = siteInfo.Name,
                    StatusCode = (int)response.StatusCode,
                    DateTime = response.Headers.Date.Value.DateTime
                };
            }
            catch (HttpRequestException e)
            {
                return new SiteResponse()
                {
                    SiteName = siteInfo.Name,
                    Message = e.Message,
                    DateTime = DateTime.Now
                };
            }
        }
    }
}
