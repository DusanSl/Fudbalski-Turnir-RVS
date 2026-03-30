using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlojPodataka.Migrations
{
    /// <inheritdoc />
    public partial class PreimenovanjeKolona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StavkeZapisnika_Klubovi_TimID",
                table: "StavkeZapisnika");

            migrationBuilder.RenameColumn(
                name: "TerenGrad",
                table: "Zapisnici",
                newName: "TerenMesto");

            migrationBuilder.RenameColumn(
                name: "TimID",
                table: "StavkeZapisnika",
                newName: "KlubID");

            migrationBuilder.RenameIndex(
                name: "IX_StavkeZapisnika_TimID",
                table: "StavkeZapisnika",
                newName: "IX_StavkeZapisnika_KlubID");

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeZapisnika_Klubovi_KlubID",
                table: "StavkeZapisnika",
                column: "KlubID",
                principalTable: "Klubovi",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StavkeZapisnika_Klubovi_KlubID",
                table: "StavkeZapisnika");

            migrationBuilder.RenameColumn(
                name: "TerenMesto",
                table: "Zapisnici",
                newName: "TerenGrad");

            migrationBuilder.RenameColumn(
                name: "KlubID",
                table: "StavkeZapisnika",
                newName: "TimID");

            migrationBuilder.RenameIndex(
                name: "IX_StavkeZapisnika_KlubID",
                table: "StavkeZapisnika",
                newName: "IX_StavkeZapisnika_TimID");

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeZapisnika_Klubovi_TimID",
                table: "StavkeZapisnika",
                column: "TimID",
                principalTable: "Klubovi",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
