using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueDepartmentLocationIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_department_location_department_id",
                table: "department_location");

            migrationBuilder.CreateIndex(
                name: "IX_department_location_department_id_location_id",
                table: "department_location",
                columns: new[] { "department_id", "location_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_department_location_department_id_location_id",
                table: "department_location");

            migrationBuilder.CreateIndex(
                name: "IX_department_location_department_id",
                table: "department_location",
                column: "department_id");
        }
    }
}
