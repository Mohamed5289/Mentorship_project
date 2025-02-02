using MentorshipHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MentorshipHub.EF.Data.ConfigurationModels
{
    public class TaskConfiguration:IEntityTypeConfiguration<TaskOfMentorship>
    {
        public void Configure(EntityTypeBuilder<TaskOfMentorship> builder)
        {
            builder.ToTable("Task");
            builder.HasKey(t => t.TaskId);
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Deadline).IsRequired();
            builder.HasOne(t => t.Mentorship).WithMany(m => m.Tasks).HasForeignKey(t => t.MentorshipId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.TaskSubmissions).WithOne(t => t.Task).HasForeignKey(t => t.TaskId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
