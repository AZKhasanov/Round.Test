using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteMonitoring.Core.Entities.ModelDtos;
using SiteMonitoring.Core.Entities.Models;
using SiteMonitoring.Core.Monitoring.Interfaces;
using SiteMonitoring.Core.Monitoring.Services.Interfaces;
using SiteMonitoring.Data;

namespace SiteMonitoring.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MonitoringController : ControllerBase
    {
        private DataContext _dataContext;

        public MonitoringController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("sites")]
        public IActionResult GetSites()
        {
            return Ok(_dataContext.Sites.ToArray());
        }

        [HttpGet("siteStatuses")]
        public IActionResult GetSiteStatuses()
        {
            return Ok(_dataContext.SiteStatuses.Select(s => new SiteStatusDto()
            {
                Id = s.Id,
                SiteId = s.SiteId,
                SiteName = s.Site.Name,
                SiteUrl = s.Site.Url,
                RefreshInterval = s.Site.RefreshInterval,
                StatusCode = s.StatusCode,
                Message = s.Message,
                UpdateDateTime = s.UpdateDateTime
            }).ToArray());
        }


    [HttpGet("getStatus/{name}")]
    public async Task<IActionResult> GetSiteStatus(string name)
    {
        var status = _dataContext.SiteStatuses.Where(s => s.Site.Name == name).Select(s => new SiteStatusDto()
        {
            Id = s.Id,
            SiteId = s.SiteId,
            SiteName = s.Site.Name,
            SiteUrl = s.Site.Url,
            RefreshInterval = s.Site.RefreshInterval,
            StatusCode = s.StatusCode,
            Message = s.Message,
            UpdateDateTime = s.UpdateDateTime
        }).SingleOrDefault();

        if (status != null)
            return Ok(status);
        else
            return NotFound();
    }

    [Authorize(Roles = "admin")]
    [HttpPost("addSite")]
    public async Task<IActionResult> AddSite([FromBody] SiteInfo siteInfo)
    {
        var site = _dataContext.Sites.Where(s => s.Name == siteInfo.Name).SingleOrDefault();
        if (site == null)
        {
            await _dataContext.Sites.AddAsync(new Site()
            {
                Name = siteInfo.Name,
                Url = siteInfo.Url
            });
            await _dataContext.SaveChangesAsync();
            site = _dataContext.Sites.Where(s => s.Name == siteInfo.Name).SingleOrDefault();
            return Ok(site);
        }
        else
            return Conflict();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("removeSite/{name}")]
    public async Task<IActionResult> RemoveSite(string name)
    {
        var site = _dataContext.Sites.Where(s => s.Name == name).SingleOrDefault();
        if (site != null)
        {
            _dataContext.Sites.Remove(site);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
        else
            return NotFound();
    }
}
}
