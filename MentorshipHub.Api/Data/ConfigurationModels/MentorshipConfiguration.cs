using MentorshipHub.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorshipHub.Api.Data.ConfigurationModels
{
    public class MentorshipConfiguration : IEntityTypeConfiguration<Mentorship>
    {
        public void Configure(EntityTypeBuilder<Mentorship> builder)
        {
            builder.HasKey(m => m.MentorshipId);
            builder.Property(m => m.Discription).IsRequired();
            builder.Property(m => m.Title).IsRequired();
            builder.Property(m => m.status).IsRequired();
            builder.Property(m => m.Hours).IsRequired();
            builder.Property(m => m.StartDate).IsRequired();
            builder.Property(m => m.EndDate).IsRequired();
            builder.HasOne(m => m.Mentor).WithMany(m => m.Mentorships).HasForeignKey(m => m.MentorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(m => m.Achievements).WithOne(m => m.Mentorship).HasForeignKey(m => m.MentorshipId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.Mentees).WithMany(m => m.Mentorships).UsingEntity<Achievement>();
            builder.HasMany(mr => mr.MentorshipRegistrations).WithOne(m => m.Mentorship).HasForeignKey(m => m.MentorshipId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
