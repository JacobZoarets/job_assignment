using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configure the table name for the User entity
            builder.ToTable("Users");

            // Configure the primary key for the User entity
            builder.HasKey(u => u.Id);

            // Configure properties for the User entity
            builder.Property(u => u.FirstName)
                .IsRequired() // First name is required
                .HasMaxLength(50); // Maximum length of 50 characters

            builder.Property(u => u.LastName)
                .IsRequired() // Last name is required
                .HasMaxLength(50); // Maximum length of 50 characters

            builder.Property(u => u.Email)
                .IsRequired() // Email is required
                .HasMaxLength(100); // Maximum length of 100 characters

            builder.Property(u => u.DateOfBirth)
                .IsRequired(); // Date of birth is required

            builder.Property(u => u.Phone)
                .HasMaxLength(15); // Maximum length of 15 characters

            builder.Property(u => u.Address)
                .HasMaxLength(250); // Maximum length of 250 characters

            builder.Property(u => u.ProfilePicture)
                .HasMaxLength(250); // Maximum length of 250 characters for profile picture URL
        }
    }
}