using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace online_job_finder.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Locations",
                columns: table => new
                {
                    Location_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Locations", x => x.Location_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Skills",
                columns: table => new
                {
                    Skill_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Skills", x => x.Skill_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Locations");

            migrationBuilder.DropTable(
                name: "Tbl_Skills");
        }
    }
}
