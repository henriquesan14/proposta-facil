using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RequestUpgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PendingUpgradePlanId",
                table: "Subscriptions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PendingUpgradePlanId",
                table: "Subscriptions",
                column: "PendingUpgradePlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionPlans_PendingUpgradePlanId",
                table: "Subscriptions",
                column: "PendingUpgradePlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionPlans_PendingUpgradePlanId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PendingUpgradePlanId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PendingUpgradePlanId",
                table: "Subscriptions");
        }
    }
}
