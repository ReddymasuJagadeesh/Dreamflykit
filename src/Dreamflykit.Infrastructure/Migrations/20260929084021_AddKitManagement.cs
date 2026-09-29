using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dreamflykit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddKitManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kits",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Mrp = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    SellingPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kits_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "public",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kits_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalSchema: "public",
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KitItems",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KitId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductVariantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitItems_Kits_KitId",
                        column: x => x.KitId,
                        principalSchema: "public",
                        principalTable: "Kits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KitItems_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalSchema: "public",
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KitItems_KitId_DisplayOrder",
                schema: "public",
                table: "KitItems",
                columns: new[] { "KitId", "DisplayOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_KitItems_KitId_ProductVariantId",
                schema: "public",
                table: "KitItems",
                columns: new[] { "KitId", "ProductVariantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KitItems_ProductVariantId",
                schema: "public",
                table: "KitItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_CountryId_SeasonId_IsActive",
                schema: "public",
                table: "Kits",
                columns: new[] { "CountryId", "SeasonId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Kits_CountryId_SeasonId_Size",
                schema: "public",
                table: "Kits",
                columns: new[] { "CountryId", "SeasonId", "Size" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kits_DisplayOrder",
                schema: "public",
                table: "Kits",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_SeasonId",
                schema: "public",
                table: "Kits",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_Slug",
                schema: "public",
                table: "Kits",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KitItems",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Kits",
                schema: "public");
        }
    }
}
