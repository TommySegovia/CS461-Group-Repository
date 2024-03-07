using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeakPals_Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Climber",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ASPNetIdentityId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(1600)", maxLength: 1600, nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Climber__3214EC27495224C8", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FitnessTest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FitnessT__3214EC27C00405FE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FitnessDataEntry",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClimberID = table.Column<int>(type: "int", nullable: true),
                    TestID = table.Column<int>(type: "int", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: true),
                    BodyWeight = table.Column<int>(type: "int", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FitnessD__3214EC27A69CA072", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FitnessDataEntry_Climber_ID",
                        column: x => x.ClimberID,
                        principalTable: "Climber",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FitnessDataEntry_Test_ID",
                        column: x => x.TestID,
                        principalTable: "FitnessTest",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FitnessDataEntry_ClimberID",
                table: "FitnessDataEntry",
                column: "ClimberID");

            migrationBuilder.CreateIndex(
                name: "IX_FitnessDataEntry_TestID",
                table: "FitnessDataEntry",
                column: "TestID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitnessDataEntry");

            migrationBuilder.DropTable(
                name: "Climber");

            migrationBuilder.DropTable(
                name: "FitnessTest");
        }
    }
}
