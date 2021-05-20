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
    public class SiteStatusConfiguration: IEntityTypeConfiguration<SiteStatus>
    {
        public void Configure(EntityTypeBuilder<SiteStatus> builder)
        {
            builder.ToTable("SiteStatus", "dbo");
            builder.Property(p => p.Id).HasColumnName("SiteStatusId");
            builder.Property(p => p.StatusCode).HasColumnName("StatusCode");
            builder.Property(p => p.UpdateDateTime).HasColumnName("UpdateDateTime");
            builder.Property(p => p.Message).HasColumnName("Message");

            builder.HasOne(p => p.Site).WithMany(p => p.SiteStatuses).HasForeignKey(p => p.SiteId);
        }
    }
}
