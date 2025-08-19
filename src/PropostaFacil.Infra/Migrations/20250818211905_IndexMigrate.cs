using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropostaFacil.Infra.Migrations
{
    /// <inheritdoc />
    public partial class IndexMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Contact_Email",
                table: "Users",
                column: "Contact_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_Number",
                table: "Proposals",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Contact_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_Number",
                table: "Proposals");
        }
    }
}
