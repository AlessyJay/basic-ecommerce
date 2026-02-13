using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace basic_ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class RefreshExpiresAtAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RefreshExpiresAt",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshExpiresAt",
                table: "users");
        }
    }
}
