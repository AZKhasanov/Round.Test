using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SiteMonitoring.Core.Entities.Models;
using SiteMonitoring.Core.Monitoring.Interfaces;
using SiteMonitoring.Core.Monitoring.Services.Interfaces;

namespace SiteMonitoring.Core.Monitoring
{
    public class SiteMonitoringManager
    {
        private ISiteMonitorService _monitorService;
        private IMonitoringDataContext _monitoringDataContext;
        private Dictionary<string, SiteInfo> _sites = new();
        private Dictionary<string, Timer> _timers = new();

        public SiteMonitoringManager(ISiteMonitorService siteMonitorService, IMonitoringDataContext monitoringDataContext)
        {
            _monitorService = siteMonitorService;
            _monitoringDataContext = monitoringDataContext;
            LoadSiteList();
        }

        private void LoadSiteList()
        {
            _sites.Clear();
            _timers.Clear();
            foreach (var site in _monitoringDataContext.GetSites())
                AddSite(site);
        }

        private void ReloadSitesList()
        {
            LoadSiteList();
        }

        private void AddSite(SiteInfo siteInfo)
        {
            if (!_sites.ContainsKey(siteInfo.Name))
            {
                _sites[siteInfo.Name] = siteInfo;

                var timer = new Timer(TimerCallback, siteInfo, 0, siteInfo.RefreshInterval);
                _timers[siteInfo.Name] = timer;
            }
            else
                throw new Exception("Site already exists in monitoring list!");
        }

        private void RemoveSite(string name)
        {
            if (_sites.ContainsKey(name))
            {
                _sites.Remove(name);
                _timers.Remove(name);
            }
            else
                throw new Exception("Site not found!");
        }

        public async Task<SiteResponse> GetResponseAsync(string name)
        {
            if (_sites.TryGetValue(name, out var siteInfo))
                return await _monitorService.GetResponseAsync(siteInfo);
            else
                throw new Exception("Site not found!");
        }

        public async void TimerCallback(object? obj)
        {
            var siteInfo = (SiteInfo) obj;
            var status = await GetResponseAsync(siteInfo.Name);
            await _monitoringDataContext.SaveSiteStatusAsync(status);
        }
    }
}
