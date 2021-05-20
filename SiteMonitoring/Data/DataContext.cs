using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiteMonitoring.Core.Entities.Configurations;
using SiteMonitoring.Core.Entities.Models;

namespace SiteMonitoring.Data
{
    public class DataContext: DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteStatus> SiteStatuses { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SiteConfiguraion());
            modelBuilder.ApplyConfiguration(new SiteStatusConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
