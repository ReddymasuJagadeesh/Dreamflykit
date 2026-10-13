using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dreamflykit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerCategoryAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellerCategoryAssignments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerCategoryAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerCategoryAssignments_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "public",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellerCategoryAssignments_CategoryId",
                schema: "public",
                table: "SellerCategoryAssignments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerCategoryAssignments_SellerId",
                schema: "public",
                table: "SellerCategoryAssignments",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerCategoryAssignments_SellerId_CategoryId",
                schema: "public",
                table: "SellerCategoryAssignments",
                columns: new[] { "SellerId", "CategoryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellerCategoryAssignments",
                schema: "public");
        }
    }
}
