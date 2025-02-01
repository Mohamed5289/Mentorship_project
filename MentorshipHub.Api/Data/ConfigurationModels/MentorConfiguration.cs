using MentorshipHub.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorshipHub.Api.Data.ConfigurationModels
{
    public class MentorConfiguration : IEntityTypeConfiguration<Mentor>
    {
        public void Configure(EntityTypeBuilder<Mentor> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired();
            builder.HasOne(m => m.AppUser).WithOne(a => a.Mentor).HasForeignKey<Mentor>(m => m.AppUserId);
            builder.HasMany(m => m.Mentorships).WithOne(m => m.Mentor).HasForeignKey(m => m.MentorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.MentorshipRegistrations).WithOne(m => m.Mentor).HasForeignKey(m => m.MentorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
