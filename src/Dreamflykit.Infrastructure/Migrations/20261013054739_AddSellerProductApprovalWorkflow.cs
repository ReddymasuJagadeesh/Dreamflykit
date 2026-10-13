using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dreamflykit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerProductApprovalWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                schema: "public",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ApprovedAtUtc",
                schema: "public",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                schema: "public",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPromoted",
                schema: "public",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                schema: "public",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                schema: "public",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ApprovalStatus",
                schema: "public",
                table: "Products",
                column: "ApprovalStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsPromoted",
                schema: "public",
                table: "Products",
                column: "IsPromoted");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                schema: "public",
                table: "Products",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ApprovalStatus",
                schema: "public",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IsPromoted",
                schema: "public",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SellerId",
                schema: "public",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                schema: "public",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApprovedAtUtc",
                schema: "public",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "public",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsPromoted",
                schema: "public",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                schema: "public",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellerId",
                schema: "public",
                table: "Products");
        }
    }
}
