using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UniqueEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Contact_Email",
                table: "Tenants",
                column: "Contact_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Contact_Email",
                table: "Clients",
                column: "Contact_Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_Contact_Email",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Contact_Email",
                table: "Clients");
        }
    }
}
