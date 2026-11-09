using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dreamflykit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryHubConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryHubConfigurations",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HubSlug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    HeroBadge = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    HeroBannerUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    HeroActionText = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    HeroActionUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ConfigPayloadJson = table.Column<string>(type: "jsonb", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryHubConfigurations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryHubConfigurations_HubSlug",
                schema: "public",
                table: "CategoryHubConfigurations",
                column: "HubSlug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryHubConfigurations_IsActive",
                schema: "public",
                table: "CategoryHubConfigurations",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryHubConfigurations",
                schema: "public");
        }
    }
}
