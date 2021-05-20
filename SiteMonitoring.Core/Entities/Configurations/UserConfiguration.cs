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
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "dbo");
            builder.Property(p => p.Id).HasColumnName("UserId");
            builder.Property(p => p.Name).HasColumnName("UserName");
            builder.Property(p => p.Password).HasColumnName("UserPassword");
            builder.Property(p => p.Role).HasColumnName("UserPassword");
        }
    }
}
