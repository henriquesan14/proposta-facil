using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UniqueEmailTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_Contact_Email",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_Document_Number",
                table: "Tenants");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Contact_Email",
                table: "Tenants",
                column: "Contact_Email",
                unique: true,
                filter: "\"IsActive\" = TRUE");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Document_Number",
                table: "Tenants",
                column: "Document_Number",
                unique: true,
                filter: "\"IsActive\" = TRUE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_Contact_Email",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_Document_Number",
                table: "Tenants");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Contact_Email",
                table: "Tenants",
                column: "Contact_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Document_Number",
                table: "Tenants",
                column: "Document_Number",
                unique: true);
        }
    }
}
