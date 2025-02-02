using MentorshipHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorshipHub.EF.Data.ConfigurationModels
{
    public class MenteeConfiguration : IEntityTypeConfiguration<Mentee>
    {
        public void Configure(EntityTypeBuilder<Mentee> builder)
        {
            builder.ToTable("Mentees");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired();
            builder.HasOne(m => m.AppUser).WithOne(a => a.Mentee).HasForeignKey<Mentee>(m => m.AppUserId);
            builder.HasMany(m => m.Achievements).WithOne(a => a.Mentee).HasForeignKey(a => a.MenteeId);
            builder.HasMany(m => m.Mentorships).WithMany(m => m.Mentees).UsingEntity<Achievement>();
        }
    }
}
