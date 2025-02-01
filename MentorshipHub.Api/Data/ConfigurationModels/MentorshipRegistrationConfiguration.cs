using MentorshipHub.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorshipHub.Api.Data.ConfigurationModels
{
    public class MentorshipRegistrationConfiguration : IEntityTypeConfiguration<MentorshipRegistration>
    {
        public void Configure(EntityTypeBuilder<MentorshipRegistration> builder)
        {
           builder.HasKey(m => m.MentorshipRegistrationId);
            builder.Property(m => m.RegistrationDate).IsRequired();
            builder.Property(m => m.Status).IsRequired();
            builder.HasOne(m => m.Mentorship).WithMany(m => m.MentorshipRegistrations).HasForeignKey(m => m.MentorshipId);
            builder.HasOne(m => m.Mentee).WithMany(m => m.MentorshipRegistrations).HasForeignKey(m => m.MenteeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.Mentor).WithMany(m => m.MentorshipRegistrations).HasForeignKey(m => m.MentorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
