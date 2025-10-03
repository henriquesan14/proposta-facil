using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class DueDateMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstInvoice",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaidDate",
                table: "Payments",
                newName: "DueDate");

            migrationBuilder.AddColumn<DateOnly>(
                name: "PaymentDate",
                table: "Payments",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Payments",
                newName: "PaidDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstInvoice",
                table: "Payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
