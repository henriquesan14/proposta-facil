using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Subscription2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                table: "Subscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                table: "Subscriptions",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                table: "Subscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                table: "Subscriptions",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
