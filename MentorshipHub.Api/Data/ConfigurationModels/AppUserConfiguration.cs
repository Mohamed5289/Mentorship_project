using MentorshipHub.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorshipHub.Api.Data.ConfigurationModels
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Bio).HasMaxLength(500);

            builder.HasOne(x => x.Mentee).WithOne(x => x.AppUser).HasForeignKey<Mentee>(x => x.AppUserId);
            builder.HasOne(x => x.Mentor).WithOne(x => x.AppUser).HasForeignKey<Mentor>(x => x.AppUserId);
            builder.HasOne(x => x.Admin).WithOne(x => x.AppUser).HasForeignKey<Admin>(x => x.AppUserId);
        }
    }
}
