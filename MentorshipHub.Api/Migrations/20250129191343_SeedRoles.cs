using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorshipHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var menteeRoleId = Guid.NewGuid().ToString();
            var mentorRoleId = Guid.NewGuid().ToString();
            var adminRoleId = Guid.NewGuid().ToString();

            // Insert the Mentee role
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { menteeRoleId, "Mentee", "MENTEE", Guid.NewGuid().ToString() }
            );

            // Insert the Admin role
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { adminRoleId, "Admin", "ADMIN", Guid.NewGuid().ToString() }
            );

            // Insert the Mentor role
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { mentorRoleId, "Mentor", "MENTOR", Guid.NewGuid().ToString() }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete the Admin role
            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Name",
                keyValue: "Admin"
            );

            // Delete the Mentee role
            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Name",
                keyValue: "Mentee"
            );

            // Delete the Mentor role
            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Name",
                keyValue: "Mentor"
            );
        }
    }
}
