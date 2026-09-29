using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dreamflykit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_ProductAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "public",
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "public",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_Code",
                schema: "public",
                table: "ProductAttributes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_DisplayOrder",
                schema: "public",
                table: "ProductAttributes",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_AttributeId_Value",
                schema: "public",
                table: "ProductAttributeValues",
                columns: new[] { "AttributeId", "Value" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId_AttributeId",
                schema: "public",
                table: "ProductAttributeValues",
                columns: new[] { "ProductId", "AttributeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttributeValues",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProductAttributes",
                schema: "public");
        }
    }
}
