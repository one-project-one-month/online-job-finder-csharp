using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace online_job_finder.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeleteColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Tbl_Skills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Tbl_Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Tbl_JobCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Tbl_Skills");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Tbl_Locations");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Tbl_JobCategories");
        }
    }
}
