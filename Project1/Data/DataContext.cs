using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Router> Routers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainDriver> TrainDrivers { get; set; }
        public DbSet<DriverTrainCategory> DriverTrainCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DriverTrainCategory>()
                .HasKey(dtc => new { dtc.DriverId, dtc.TrainId });


            modelBuilder.Entity<DriverTrainCategory>()
                .HasOne(t => t.Train)
                .WithMany(dtc => dtc.DriverTrainCategory)
                .HasForeignKey(t => t.TrainId);

            modelBuilder.Entity<DriverTrainCategory>()
                .HasOne(d => d.TrainDriver)
                .WithMany(dtc => dtc.DriverTrainCategory)
                .HasForeignKey(d => d.DriverId);











            /*base.OnModelCreating(modelBuilder);
            var driver = modelBuilder.Entity<TrainDriver>();
            driver.HasKey(d => d.Id);
            driver.Property(p => p.Name);

            var train = modelBuilder.Entity<Train>();
            train.HasKey(d => d.Id);

            train.HasOne(d => d.TrainDriver)
                .WithOne(d => d.Train)
                .HasForeignKey<Train>(fk => fk.Id);*/
        }
    }
}
