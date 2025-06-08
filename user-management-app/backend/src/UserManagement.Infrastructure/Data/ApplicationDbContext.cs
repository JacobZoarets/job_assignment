using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Data
{
    // ApplicationDbContext is the main class that manages the database connection and entity sets.
    public class ApplicationDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes them to the base class.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet property for accessing User entities in the database.
        public DbSet<User> Users { get; set; }

        // OnModelCreating method to configure the model and relationships.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Apply all configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}