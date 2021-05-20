using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMonitoring.Core.Entities.Models
{
    public class SiteStatus
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public Site Site { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
