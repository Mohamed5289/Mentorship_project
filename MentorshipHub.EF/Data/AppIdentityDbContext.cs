using MentorshipHub.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MentorshipHub.EF.Data.ConfigurationModels;

namespace MentorshipHub.EF.Data
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }
        public DbSet<Mentee> Mentees { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Mentorship> Mentorships { get; set; }
        public DbSet<TaskOfMentorship> TaskOfMentorships { get; set; }
        public DbSet<TaskSubmission> TaskSubmissions { get; set; }
        public DbSet<MentorshipRegistration> MentorshipRegistrations { get; set; }
        public DbSet<Achievement> Achievements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
        }
    }
}
