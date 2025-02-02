using MentorshipHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorshipHub.EF.Data.ConfigurationModels
{
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.ToTable("Achievements");
            builder.HasKey(a => new {a.MenteeId , a.Id , a.MentorshipId});
            builder.Property(a => a.Feedback).HasMaxLength(500);
            builder.HasOne(a => a.Mentorship).WithMany(m => m.Achievements).HasForeignKey(a => a.MentorshipId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Mentee).WithMany(m => m.Achievements).HasForeignKey(a => a.MenteeId).OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
