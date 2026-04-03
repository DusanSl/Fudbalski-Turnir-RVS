using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlojPodataka.Migrations
{
    /// <inheritdoc />
    public partial class CelaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klub",
                columns: table => new
                {
                    KlubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKluba = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stadion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BrojIgraca = table.Column<int>(type: "int", nullable: false),
                    BrojOsvojenihTitula = table.Column<int>(type: "int", nullable: false),
                    GodinaOsnivanja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klub", x => x.KlubID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "Zapisnik",
                columns: table => new
                {
                    ZapisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumUtakmice = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerenNaziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TerenMesto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TerenAdresa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DomacinID = table.Column<int>(type: "int", nullable: false),
                    GostID = table.Column<int>(type: "int", nullable: false),
                    KonacanRezultatDomacin = table.Column<int>(type: "int", nullable: false),
                    KonacanRezultatGost = table.Column<int>(type: "int", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zapisnik", x => x.ZapisnikID);
                    table.ForeignKey(
                        name: "FK_Zapisnik_Klub_DomacinID",
                        column: x => x.DomacinID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zapisnik_Klub_GostID",
                        column: x => x.GostID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StavkaZapisnika",
                columns: table => new
                {
                    StavkaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZapisnikID = table.Column<int>(type: "int", nullable: false),
                    MinutGola = table.Column<int>(type: "int", nullable: false),
                    ImeStrelca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KlubID = table.Column<int>(type: "int", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaZapisnika", x => x.StavkaID);
                    table.ForeignKey(
                        name: "FK_StavkaZapisnika_Klub_KlubID",
                        column: x => x.KlubID,
                        principalTable: "Klub",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StavkaZapisnika_Zapisnik_ZapisnikID",
                        column: x => x.ZapisnikID,
                        principalTable: "Zapisnik",
                        principalColumn: "ZapisnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StavkaZapisnika_KlubID",
                table: "StavkaZapisnika",
                column: "KlubID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkaZapisnika_ZapisnikID",
                table: "StavkaZapisnika",
                column: "ZapisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Zapisnik_DomacinID",
                table: "Zapisnik",
                column: "DomacinID");

            migrationBuilder.CreateIndex(
                name: "IX_Zapisnik_GostID",
                table: "Zapisnik",
                column: "GostID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "StavkaZapisnika");

            migrationBuilder.DropTable(
                name: "Zapisnik");

            migrationBuilder.DropTable(
                name: "Klub");
        }
    }
}
