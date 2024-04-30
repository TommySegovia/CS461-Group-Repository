using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeakPals_Project.Migrations
{
    /// <inheritdoc />
    public partial class FixClimbAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FitnessTest",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "FitnessDataEntry",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClimbingExperience",
                table: "FitnessDataEntry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClimbingGrade",
                table: "FitnessDataEntry",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "FitnessDataEntry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ASPNetIdentityId",
                table: "Climber",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Climber",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomLink",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkText",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClimbAttempt",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClimberID = table.Column<int>(type: "int", nullable: false),
                    ClimbId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SuggestedGrade = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ClimberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimbAttempt", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClimbAttempt_Climber_ClimberID",
                        column: x => x.ClimberID,
                        principalTable: "Climber",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClimbAttempt_Climber_ClimberId",
                        column: x => x.ClimberId,
                        principalTable: "Climber",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClimbAttempt_ClimberId",
                table: "ClimbAttempt",
                column: "ClimberId");

            migrationBuilder.CreateIndex(
                name: "IX_ClimbAttempt_ClimberID",
                table: "ClimbAttempt",
                column: "ClimberID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClimbAttempt");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "FitnessDataEntry");

            migrationBuilder.DropColumn(
                name: "ClimbingExperience",
                table: "FitnessDataEntry");

            migrationBuilder.DropColumn(
                name: "ClimbingGrade",
                table: "FitnessDataEntry");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "FitnessDataEntry");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Climber");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Climber");

            migrationBuilder.DropColumn(
                name: "CustomLink",
                table: "Climber");

            migrationBuilder.DropColumn(
                name: "LinkText",
                table: "Climber");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Climber");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FitnessTest",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Climber",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ASPNetIdentityId",
                table: "Climber",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);
        }
    }
}
