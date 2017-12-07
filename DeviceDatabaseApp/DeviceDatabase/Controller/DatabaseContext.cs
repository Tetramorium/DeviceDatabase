using DeviceDatabase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceDatabase.Controller
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("Data Source = Database.sqlite; Version=3;")
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Calamity> Calamities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().HasKey(d => d.Id);
            modelBuilder.Entity<Calamity>()
                .HasRequired(c => c.Device)
                .WithMany(d => d.CalamitiesList)
                .HasForeignKey(d => d.Id);
        }
    }
}
