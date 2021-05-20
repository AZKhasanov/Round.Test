using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMonitoring.Core.Entities.Models
{
    public class SiteResponse
    {
        public string SiteName { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
