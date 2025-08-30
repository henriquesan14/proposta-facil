using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AsaasSubscriptionIdMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubscriptionAsaasId",
                table: "Subscriptions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionAsaasId",
                table: "Subscriptions");
        }
    }
}
