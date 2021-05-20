using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteMonitoring.Core.Entities.Models
{
    public struct SiteInfo
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int RefreshInterval { get; set; }
    }
}
