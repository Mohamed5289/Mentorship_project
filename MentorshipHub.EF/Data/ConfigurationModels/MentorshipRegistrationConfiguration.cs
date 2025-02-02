using MentorshipHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorshipHub.EF.Data.ConfigurationModels
{
    public class MentorshipRegistrationConfiguration : IEntityTypeConfiguration<MentorshipRegistration>
    {
        public void Configure(EntityTypeBuilder<MentorshipRegistration> builder)
        {
            builder.ToTable("MentorshipRegistrations");
            builder.HasKey(m => m.MentorshipRegistrationId);
            builder.Property(m => m.RegistrationDate).IsRequired();
            builder.Property(m => m.Status).IsRequired();
            builder.HasOne(m => m.Mentorship).WithMany(m => m.MentorshipRegistrations).HasForeignKey(m => m.MentorshipId);
            builder.HasOne(m => m.Mentor).WithMany(m => m.MentorshipRegistrations).HasForeignKey(m => m.MentorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.AppUser).WithMany(m => m.MentorshipRegistrations).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
