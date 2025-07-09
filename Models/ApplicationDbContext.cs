 using Microsoft.EntityFrameworkCore;

namespace Take_Home_Test___Web_App.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Absen> Absen { get; set; }

   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().ToTable("Employee");

            modelBuilder.Entity<Absen>().ToTable("Absen");


            base.OnModelCreating(modelBuilder);
        }
    }

}