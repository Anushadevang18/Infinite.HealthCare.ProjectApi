using Microsoft.EntityFrameworkCore;
namespace Infinite.HealthCare.ProjectApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //add table information
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointmnets { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<User> Users { get; set; }

  

    }
}
