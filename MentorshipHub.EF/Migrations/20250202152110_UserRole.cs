using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorshipHub.EF.Migrations
{
    /// <inheritdoc />
    public partial class UserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var userRoleId = Guid.NewGuid().ToString();
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { userRoleId, "User", "USER", Guid.NewGuid().ToString() }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            schema: "security",
            table: "Roles",
            keyColumn: "Name",
            keyValue: "User"
            );
        }
    }
}
