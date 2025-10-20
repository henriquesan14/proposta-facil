using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Subscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_TenantId",
                table: "Subscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TenantId",
                table: "Subscriptions",
                column: "TenantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                table: "Subscriptions",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_TenantId",
                table: "Subscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TenantId",
                table: "Subscriptions",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                table: "Subscriptions",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
