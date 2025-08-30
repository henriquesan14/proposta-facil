using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class LinksAsaasMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AsaasId",
                table: "Tenants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentLink",
                table: "SubscriptionPlans",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsaasId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "PaymentLink",
                table: "SubscriptionPlans");
        }
    }
}
