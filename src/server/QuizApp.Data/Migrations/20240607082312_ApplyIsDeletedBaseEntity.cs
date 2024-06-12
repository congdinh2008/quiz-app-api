using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplyIsDeletedBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "security",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "security",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "common",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "common",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "common",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "security",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "common",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "common",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "common",
                table: "Answers");
        }
    }
}
