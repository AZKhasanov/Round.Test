using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMonitoring.Core.Entities.ModelDtos
{
    public class SiteStatusDto
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public int RefreshInterval { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
