using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMonitoring.Core.Entities.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int RefreshInterval { get; set; }

        public IList<SiteStatus> SiteStatuses { get; set; } 
    }
}
