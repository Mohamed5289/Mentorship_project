using MentorshipHub.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MentorshipHub.Api.Data.ConfigurationModels
{
    public class TaskConfiguration : IEntityTypeConfiguration<Models.Task>
    {
        public void Configure(EntityTypeBuilder<Models.Task> builder)
        {
            builder.HasKey(t => t.TaskId);
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Deadline).IsRequired();
            builder.HasOne(t => t.Mentorship).WithMany(m => m.Tasks).HasForeignKey(t => t.MentorshipId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.TaskSubmissions).WithOne(t => t.Task).HasForeignKey(t => t.TaskId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
