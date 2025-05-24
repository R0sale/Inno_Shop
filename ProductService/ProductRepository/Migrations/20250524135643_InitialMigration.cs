using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accessibility = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Accessibility", "CreationDate", "Description", "Name", "OwnerId", "Price" },
                values: new object[,]
                {
                    { new Guid("3d420a70-94ce-3d15-9494-5258280c2ce3"), true, new DateTime(2022, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Orange fruit", "Orange", new Guid("3d290a70-94ce-4d15-9292-5248280c2ce3"), 15.1m },
                    { new Guid("3d440a70-94ce-4d15-9494-5244240c2ce3"), true, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Green fruit", "Pear", new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 100.1m },
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), true, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yellow fruit", "Banana", new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 18.1m },
                    { new Guid("3d490a71-94ce-4d12-9494-5248280c2ce3"), true, new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red fruit", "Apple", new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 12.1m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
