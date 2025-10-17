using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class createdByName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Tenants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Subscriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "SubscriptionPlans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "RefreshTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Proposals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "ProposalItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Payments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Clients",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "ProposalItems");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Clients");
        }
    }
}
