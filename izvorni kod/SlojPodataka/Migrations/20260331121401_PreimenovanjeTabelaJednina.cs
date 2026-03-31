using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlojPodataka.Migrations
{
    /// <inheritdoc />
    public partial class PreimenovanjeTabelaJednina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StavkeZapisnika_Klubovi_KlubID",
                table: "StavkeZapisnika");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkeZapisnika_Zapisnici_ZapisnikID",
                table: "StavkeZapisnika");

            migrationBuilder.DropForeignKey(
                name: "FK_Zapisnici_Klubovi_DomacinID",
                table: "Zapisnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Zapisnici_Klubovi_GostID",
                table: "Zapisnici");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zapisnici",
                table: "Zapisnici");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StavkeZapisnika",
                table: "StavkeZapisnika");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Korisnici",
                table: "Korisnici");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klubovi",
                table: "Klubovi");

            migrationBuilder.RenameTable(
                name: "Zapisnici",
                newName: "Zapisnik");

            migrationBuilder.RenameTable(
                name: "StavkeZapisnika",
                newName: "StavkaZapisnika");

            migrationBuilder.RenameTable(
                name: "Korisnici",
                newName: "Korisnik");

            migrationBuilder.RenameTable(
                name: "Klubovi",
                newName: "Klub");

            migrationBuilder.RenameIndex(
                name: "IX_Zapisnici_GostID",
                table: "Zapisnik",
                newName: "IX_Zapisnik_GostID");

            migrationBuilder.RenameIndex(
                name: "IX_Zapisnici_DomacinID",
                table: "Zapisnik",
                newName: "IX_Zapisnik_DomacinID");

            migrationBuilder.RenameIndex(
                name: "IX_StavkeZapisnika_ZapisnikID",
                table: "StavkaZapisnika",
                newName: "IX_StavkaZapisnika_ZapisnikID");

            migrationBuilder.RenameIndex(
                name: "IX_StavkeZapisnika_KlubID",
                table: "StavkaZapisnika",
                newName: "IX_StavkaZapisnika_KlubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zapisnik",
                table: "Zapisnik",
                column: "ZapisnikID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StavkaZapisnika",
                table: "StavkaZapisnika",
                column: "StavkaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Korisnik",
                table: "Korisnik",
                column: "KorisnikID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klub",
                table: "Klub",
                column: "KlubID");

            migrationBuilder.AddForeignKey(
                name: "FK_StavkaZapisnika_Klub_KlubID",
                table: "StavkaZapisnika",
                column: "KlubID",
                principalTable: "Klub",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StavkaZapisnika_Zapisnik_ZapisnikID",
                table: "StavkaZapisnika",
                column: "ZapisnikID",
                principalTable: "Zapisnik",
                principalColumn: "ZapisnikID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zapisnik_Klub_DomacinID",
                table: "Zapisnik",
                column: "DomacinID",
                principalTable: "Klub",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zapisnik_Klub_GostID",
                table: "Zapisnik",
                column: "GostID",
                principalTable: "Klub",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StavkaZapisnika_Klub_KlubID",
                table: "StavkaZapisnika");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkaZapisnika_Zapisnik_ZapisnikID",
                table: "StavkaZapisnika");

            migrationBuilder.DropForeignKey(
                name: "FK_Zapisnik_Klub_DomacinID",
                table: "Zapisnik");

            migrationBuilder.DropForeignKey(
                name: "FK_Zapisnik_Klub_GostID",
                table: "Zapisnik");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zapisnik",
                table: "Zapisnik");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StavkaZapisnika",
                table: "StavkaZapisnika");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Korisnik",
                table: "Korisnik");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klub",
                table: "Klub");

            migrationBuilder.RenameTable(
                name: "Zapisnik",
                newName: "Zapisnici");

            migrationBuilder.RenameTable(
                name: "StavkaZapisnika",
                newName: "StavkeZapisnika");

            migrationBuilder.RenameTable(
                name: "Korisnik",
                newName: "Korisnici");

            migrationBuilder.RenameTable(
                name: "Klub",
                newName: "Klubovi");

            migrationBuilder.RenameIndex(
                name: "IX_Zapisnik_GostID",
                table: "Zapisnici",
                newName: "IX_Zapisnici_GostID");

            migrationBuilder.RenameIndex(
                name: "IX_Zapisnik_DomacinID",
                table: "Zapisnici",
                newName: "IX_Zapisnici_DomacinID");

            migrationBuilder.RenameIndex(
                name: "IX_StavkaZapisnika_ZapisnikID",
                table: "StavkeZapisnika",
                newName: "IX_StavkeZapisnika_ZapisnikID");

            migrationBuilder.RenameIndex(
                name: "IX_StavkaZapisnika_KlubID",
                table: "StavkeZapisnika",
                newName: "IX_StavkeZapisnika_KlubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zapisnici",
                table: "Zapisnici",
                column: "ZapisnikID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StavkeZapisnika",
                table: "StavkeZapisnika",
                column: "StavkaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Korisnici",
                table: "Korisnici",
                column: "KorisnikID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klubovi",
                table: "Klubovi",
                column: "KlubID");

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeZapisnika_Klubovi_KlubID",
                table: "StavkeZapisnika",
                column: "KlubID",
                principalTable: "Klubovi",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeZapisnika_Zapisnici_ZapisnikID",
                table: "StavkeZapisnika",
                column: "ZapisnikID",
                principalTable: "Zapisnici",
                principalColumn: "ZapisnikID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zapisnici_Klubovi_DomacinID",
                table: "Zapisnici",
                column: "DomacinID",
                principalTable: "Klubovi",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zapisnici_Klubovi_GostID",
                table: "Zapisnici",
                column: "GostID",
                principalTable: "Klubovi",
                principalColumn: "KlubID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
