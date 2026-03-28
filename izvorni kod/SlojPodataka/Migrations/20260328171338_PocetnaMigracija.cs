using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlojPodataka.Migrations
{
    /// <inheritdoc />
    public partial class PocetnaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klubovi",
                columns: table => new
                {
                    KlubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKluba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stadion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojIgraca = table.Column<int>(type: "int", nullable: false),
                    BrojOsvojenihTitula = table.Column<int>(type: "int", nullable: false),
                    GodinaOsnivanja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klubovi", x => x.KlubID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "Zapisnici",
                columns: table => new
                {
                    ZapisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumUtakmice = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerenNaziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerenGrad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerenAdresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomacinID = table.Column<int>(type: "int", nullable: false),
                    GostID = table.Column<int>(type: "int", nullable: false),
                    KonacanRezultatDomacin = table.Column<int>(type: "int", nullable: false),
                    KonacanRezultatGost = table.Column<int>(type: "int", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zapisnici", x => x.ZapisnikID);
                    table.ForeignKey(
                        name: "FK_Zapisnici_Klubovi_DomacinID",
                        column: x => x.DomacinID,
                        principalTable: "Klubovi",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zapisnici_Klubovi_GostID",
                        column: x => x.GostID,
                        principalTable: "Klubovi",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StavkeZapisnika",
                columns: table => new
                {
                    StavkaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZapisnikID = table.Column<int>(type: "int", nullable: false),
                    MinutGola = table.Column<int>(type: "int", nullable: false),
                    ImeStrelca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimID = table.Column<int>(type: "int", nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeZapisnika", x => x.StavkaID);
                    table.ForeignKey(
                        name: "FK_StavkeZapisnika_Klubovi_TimID",
                        column: x => x.TimID,
                        principalTable: "Klubovi",
                        principalColumn: "KlubID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StavkeZapisnika_Zapisnici_ZapisnikID",
                        column: x => x.ZapisnikID,
                        principalTable: "Zapisnici",
                        principalColumn: "ZapisnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StavkeZapisnika_TimID",
                table: "StavkeZapisnika",
                column: "TimID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeZapisnika_ZapisnikID",
                table: "StavkeZapisnika",
                column: "ZapisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Zapisnici_DomacinID",
                table: "Zapisnici",
                column: "DomacinID");

            migrationBuilder.CreateIndex(
                name: "IX_Zapisnici_GostID",
                table: "Zapisnici",
                column: "GostID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "StavkeZapisnika");

            migrationBuilder.DropTable(
                name: "Zapisnici");

            migrationBuilder.DropTable(
                name: "Klubovi");
        }
    }
}
