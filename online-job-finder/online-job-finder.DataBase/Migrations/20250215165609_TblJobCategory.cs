using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace online_job_finder.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class TblJobCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_JobCategories",
                columns: table => new
                {
                    JobCategeory_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_JobCategories", x => x.JobCategeory_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_JobCategories");
        }
    }
}
