using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErpApp.TruckModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "truckModule");

            migrationBuilder.CreateTable(
                name: "Trucks",
                schema: "truckModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptiveData_Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DescriptiveData_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DescriptiveData_Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TruckId", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_DescriptiveData_Code",
                schema: "truckModule",
                table: "Trucks",
                column: "DescriptiveData_Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trucks",
                schema: "truckModule");
        }
    }
}
