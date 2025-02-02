using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorshipHub.EF.Migrations
{
    /// <inheritdoc />
    public partial class UserService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MentorshipRegistrations_Mentees_MenteeId",
                table: "MentorshipRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_MentorshipRegistrations_MenteeId",
                table: "MentorshipRegistrations");

            migrationBuilder.DropColumn(
                name: "MenteeId",
                table: "MentorshipRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MentorshipRegistrations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "Achievements",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_MentorshipRegistrations_UserId",
                table: "MentorshipRegistrations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MentorshipRegistrations_Users_UserId",
                table: "MentorshipRegistrations",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MentorshipRegistrations_Users_UserId",
                table: "MentorshipRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_MentorshipRegistrations_UserId",
                table: "MentorshipRegistrations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MentorshipRegistrations");

            migrationBuilder.AddColumn<int>(
                name: "MenteeId",
                table: "MentorshipRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateIndex(
                name: "IX_MentorshipRegistrations_MenteeId",
                table: "MentorshipRegistrations",
                column: "MenteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MentorshipRegistrations_Mentees_MenteeId",
                table: "MentorshipRegistrations",
                column: "MenteeId",
                principalTable: "Mentees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
