using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dreamflykit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditLogsAndEnforceUserForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ActorType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TableName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RecordId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OldValues = table.Column<string>(type: "text", nullable: true),
                    NewValues = table.Column<string>(type: "text", nullable: true),
                    AffectedColumns = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TraceId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    ErrorMessage = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    OccurredAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_ProductVariants_Mrp_NonNegative",
                schema: "public",
                table: "ProductVariants",
                sql: "\"Mrp\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ProductVariants_SellingPrice_Lte_Mrp",
                schema: "public",
                table: "ProductVariants",
                sql: "\"SellingPrice\" <= \"Mrp\"");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ProductVariants_SellingPrice_NonNegative",
                schema: "public",
                table: "ProductVariants",
                sql: "\"SellingPrice\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItems_Mrp_NonNegative",
                schema: "public",
                table: "OrderItems",
                sql: "\"Mrp\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItems_Quantity_Positive",
                schema: "public",
                table: "OrderItems",
                sql: "\"Quantity\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItems_UnitPrice_NonNegative",
                schema: "public",
                table: "OrderItems",
                sql: "\"UnitPrice\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_InventoryItems_AvailableQuantity_NonNegative",
                schema: "public",
                table: "InventoryItems",
                sql: "\"AvailableQuantity\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_InventoryItems_ReservedQuantity_NonNegative",
                schema: "public",
                table: "InventoryItems",
                sql: "\"ReservedQuantity\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CartItems_Quantity_Positive",
                schema: "public",
                table: "CartItems",
                sql: "\"Quantity\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CartItems_UnitPrice_NonNegative",
                schema: "public",
                table: "CartItems",
                sql: "\"UnitPrice\" >= 0");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Action_OccurredAtUtc",
                schema: "public",
                table: "AuditLogs",
                columns: new[] { "Action", "OccurredAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityName_RecordId",
                schema: "public",
                table: "AuditLogs",
                columns: new[] { "EntityName", "RecordId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_OccurredAtUtc",
                schema: "public",
                table: "AuditLogs",
                column: "OccurredAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId_OccurredAtUtc",
                schema: "public",
                table: "AuditLogs",
                columns: new[] { "UserId", "OccurredAtUtc" });

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                schema: "public",
                table: "Carts",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_AspNetUsers_UserId",
                schema: "public",
                table: "CustomerAddresses",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaAuditLogs_AspNetUsers_UserId",
                schema: "public",
                table: "MediaAuditLogs",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPermissions_AspNetUsers_UserId",
                schema: "public",
                table: "MediaPermissions",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                schema: "public",
                table: "Orders",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                schema: "public",
                table: "Products",
                column: "SellerId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_UserId",
                schema: "public",
                table: "Wishlists",
                column: "UserId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                schema: "public",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_AspNetUsers_UserId",
                schema: "public",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaAuditLogs_AspNetUsers_UserId",
                schema: "public",
                table: "MediaAuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaPermissions_AspNetUsers_UserId",
                schema: "public",
                table: "MediaPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                schema: "public",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_SellerId",
                schema: "public",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_UserId",
                schema: "public",
                table: "Wishlists");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "public");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductVariants_Mrp_NonNegative",
                schema: "public",
                table: "ProductVariants");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductVariants_SellingPrice_Lte_Mrp",
                schema: "public",
                table: "ProductVariants");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductVariants_SellingPrice_NonNegative",
                schema: "public",
                table: "ProductVariants");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItems_Mrp_NonNegative",
                schema: "public",
                table: "OrderItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItems_Quantity_Positive",
                schema: "public",
                table: "OrderItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItems_UnitPrice_NonNegative",
                schema: "public",
                table: "OrderItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_InventoryItems_AvailableQuantity_NonNegative",
                schema: "public",
                table: "InventoryItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_InventoryItems_ReservedQuantity_NonNegative",
                schema: "public",
                table: "InventoryItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CartItems_Quantity_Positive",
                schema: "public",
                table: "CartItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CartItems_UnitPrice_NonNegative",
                schema: "public",
                table: "CartItems");
        }
    }
}
