using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dreamflykit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaManagementModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageMediaAssetId",
                schema: "public",
                table: "Subcategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MediaAssetId",
                schema: "public",
                table: "ProductMedia",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageMediaAssetId",
                schema: "public",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MediaAssets",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaType = table.Column<int>(type: "integer", nullable: false),
                    ObjectKey = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FileExtension = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    AltText = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Caption = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaAuditLogs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MediaAssetId = table.Column<Guid>(type: "uuid", nullable: true),
                    ResourceType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ResourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    OldValue = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    NewValue = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    OccurredAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaPermissions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaType = table.Column<int>(type: "integer", nullable: false),
                    PermissionType = table.Column<int>(type: "integer", nullable: false),
                    IsGranted = table.Column<bool>(type: "boolean", nullable: false),
                    GrantedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    GrantedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAssetId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CtaText = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CtaUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    StartDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banners_MediaAssets_MediaAssetId",
                        column: x => x.MediaAssetId,
                        principalSchema: "public",
                        principalTable: "MediaAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "HomepageSections",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionKey = table.Column<int>(type: "integer", nullable: false),
                    MediaAssetId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    LinkUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    StartDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomepageSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomepageSections_MediaAssets_MediaAssetId",
                        column: x => x.MediaAssetId,
                        principalSchema: "public",
                        principalTable: "MediaAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LandingPages",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    HeroMediaAssetId = table.Column<Guid>(type: "uuid", nullable: true),
                    MetaTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    MetaDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StartDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandingPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LandingPages_MediaAssets_HeroMediaAssetId",
                        column: x => x.HeroMediaAssetId,
                        principalSchema: "public",
                        principalTable: "MediaAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_ImageMediaAssetId",
                schema: "public",
                table: "Subcategories",
                column: "ImageMediaAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMedia_MediaAssetId",
                schema: "public",
                table: "ProductMedia",
                column: "MediaAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ImageMediaAssetId",
                schema: "public",
                table: "Categories",
                column: "ImageMediaAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_DisplayOrder",
                schema: "public",
                table: "Banners",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_IsActive_IsPublished",
                schema: "public",
                table: "Banners",
                columns: new[] { "IsActive", "IsPublished" });

            migrationBuilder.CreateIndex(
                name: "IX_Banners_MediaAssetId",
                schema: "public",
                table: "Banners",
                column: "MediaAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_StartDateUtc_EndDateUtc",
                schema: "public",
                table: "Banners",
                columns: new[] { "StartDateUtc", "EndDateUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_HomepageSections_DisplayOrder",
                schema: "public",
                table: "HomepageSections",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_HomepageSections_MediaAssetId",
                schema: "public",
                table: "HomepageSections",
                column: "MediaAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_HomepageSections_SectionKey",
                schema: "public",
                table: "HomepageSections",
                column: "SectionKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LandingPages_HeroMediaAssetId",
                schema: "public",
                table: "LandingPages",
                column: "HeroMediaAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_LandingPages_Slug",
                schema: "public",
                table: "LandingPages",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LandingPages_Status",
                schema: "public",
                table: "LandingPages",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAssets_CreatedAtUtc",
                schema: "public",
                table: "MediaAssets",
                column: "CreatedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAssets_MediaType",
                schema: "public",
                table: "MediaAssets",
                column: "MediaType");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAssets_ObjectKey",
                schema: "public",
                table: "MediaAssets",
                column: "ObjectKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaAssets_Status",
                schema: "public",
                table: "MediaAssets",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAssets_UploadedBy",
                schema: "public",
                table: "MediaAssets",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAuditLogs_MediaAssetId",
                schema: "public",
                table: "MediaAuditLogs",
                column: "MediaAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAuditLogs_OccurredAtUtc",
                schema: "public",
                table: "MediaAuditLogs",
                column: "OccurredAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAuditLogs_ResourceType",
                schema: "public",
                table: "MediaAuditLogs",
                column: "ResourceType");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAuditLogs_UserId",
                schema: "public",
                table: "MediaAuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPermissions_UserId",
                schema: "public",
                table: "MediaPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPermissions_UserId_MediaType_PermissionType",
                schema: "public",
                table: "MediaPermissions",
                columns: new[] { "UserId", "MediaType", "PermissionType" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MediaAssets_ImageMediaAssetId",
                schema: "public",
                table: "Categories",
                column: "ImageMediaAssetId",
                principalSchema: "public",
                principalTable: "MediaAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMedia_MediaAssets_MediaAssetId",
                schema: "public",
                table: "ProductMedia",
                column: "MediaAssetId",
                principalSchema: "public",
                principalTable: "MediaAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_MediaAssets_ImageMediaAssetId",
                schema: "public",
                table: "Subcategories",
                column: "ImageMediaAssetId",
                principalSchema: "public",
                principalTable: "MediaAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MediaAssets_ImageMediaAssetId",
                schema: "public",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMedia_MediaAssets_MediaAssetId",
                schema: "public",
                table: "ProductMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_MediaAssets_ImageMediaAssetId",
                schema: "public",
                table: "Subcategories");

            migrationBuilder.DropTable(
                name: "Banners",
                schema: "public");

            migrationBuilder.DropTable(
                name: "HomepageSections",
                schema: "public");

            migrationBuilder.DropTable(
                name: "LandingPages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MediaAuditLogs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MediaPermissions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MediaAssets",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_ImageMediaAssetId",
                schema: "public",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductMedia_MediaAssetId",
                schema: "public",
                table: "ProductMedia");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ImageMediaAssetId",
                schema: "public",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageMediaAssetId",
                schema: "public",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "MediaAssetId",
                schema: "public",
                table: "ProductMedia");

            migrationBuilder.DropColumn(
                name: "ImageMediaAssetId",
                schema: "public",
                table: "Categories");
        }
    }
}
