using MVCWithWebService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MVCWithWebService.DAL
{
    public class GymContext : DbContext
    {

        public GymContext() : base("GymContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Facility> Facilities { get; set; } 
        public DbSet<Trainer> Trainers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Track>()
                .HasMany(c => c.Trainers).WithMany(i => i.Track)
                .Map(t => t.MapLeftKey("TrackID")
                    .MapRightKey("TrainerID")
                    .ToTable("TrackTrainer"));

            modelBuilder.Entity<Facility>().MapToStoredProcedures();
        }
    }
}