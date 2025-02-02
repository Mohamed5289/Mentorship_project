using MentorshipHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MentorshipHub.EF.Data.ConfigurationModels
{
    public class TaskSubmissionConfiguration : IEntityTypeConfiguration<TaskSubmission>
    {
        public void Configure(EntityTypeBuilder<TaskSubmission> builder)
        {
            builder.ToTable("TaskSubmissions");
            builder.HasKey(t => t.TaskSubmissionId);
            builder.Property(t => t.DueDate).IsRequired();
            builder.Property(t => t.Status).IsRequired();
            builder.Property(t => t.Feedback).IsRequired();
            builder.Property(t => t.Grade).IsRequired();
            builder.Property(t => t.Solution).IsRequired();
            builder.HasOne(t => t.Task).WithMany(m => m.TaskSubmissions).HasForeignKey(t => t.TaskId).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(t => t.Mentee).WithMany(m => m.TaskSubmissions).HasForeignKey(t => t.MenteeId).OnDelete(DeleteBehavior.Restrict);
        }

    }
}
