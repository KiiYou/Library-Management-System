using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class db6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysOverdue",
                table: "Penalties");

            migrationBuilder.RenameColumn(
                name: "PenaltyAmount",
                table: "Penalties",
                newName: "Amount");

            migrationBuilder.AddColumn<DateTime>(
                name: "PenaltyDate",
                table: "Penalties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Checkouts",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PenaltyDate",
                table: "Penalties");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Checkouts");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Penalties",
                newName: "PenaltyAmount");

            migrationBuilder.AddColumn<int>(
                name: "DaysOverdue",
                table: "Penalties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
