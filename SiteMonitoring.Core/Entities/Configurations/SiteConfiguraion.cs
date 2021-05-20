using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteMonitoring.Core.Entities.Models;

namespace SiteMonitoring.Core.Entities.Configurations
{
    public class SiteConfiguraion: IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.ToTable("Site", "dbo");
            builder.Property(p => p.Id).HasColumnName("SiteId");
            builder.Property(p => p.Name).HasColumnName("SiteName");
            builder.Property(p => p.Url).HasColumnName("SiteUrl");
            builder.Property(p => p.RefreshInterval).HasColumnName("RefreshInterval");
        }
    }
}
