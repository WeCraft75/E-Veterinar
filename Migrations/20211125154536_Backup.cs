using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Veterinar.Migrations
{
    public partial class Backup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IZDELEK",
                columns: table => new
                {
                    ID_IZDELEK = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    IME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CENA = table.Column<decimal>(type: "money", nullable: false),
                    OPIS = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IZDELEK", x => x.ID_IZDELEK)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "POSTA",
                columns: table => new
                {
                    STEVILKA = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NAZIV = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSTA", x => x.STEVILKA)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "STORITEV",
                columns: table => new
                {
                    ID_STORITEV = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    OPIS_STORITVE = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STORITEV", x => x.ID_STORITEV)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STRANKA",
                columns: table => new
                {
                    ID_STRANKA = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    STEVILKA = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    IME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PRIIMEK = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    NASLOV = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    KRAJ = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    AspNetIDId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STRANKA", x => x.ID_STRANKA)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_STRANKA_AspNetUsers_AspNetIDId",
                        column: x => x.AspNetIDId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STRANKA_JE_NA_POSTA",
                        column: x => x.STEVILKA,
                        principalTable: "POSTA",
                        principalColumn: "STEVILKA");
                });

            migrationBuilder.CreateTable(
                name: "VETERINAR",
                columns: table => new
                {
                    ID_VETERINAR = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    AspNetIDId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    STEVILKA = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    IME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PRIIMEK = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    KRAJ = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    NA_DOM = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VETERINAR", x => x.ID_VETERINAR)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_VETERINA_IMA_VETER_POSTA",
                        column: x => x.STEVILKA,
                        principalTable: "POSTA",
                        principalColumn: "STEVILKA");
                    table.ForeignKey(
                        name: "FK_VETERINAR_AspNetUsers_AspNetIDId",
                        column: x => x.AspNetIDId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NAROCILO",
                columns: table => new
                {
                    ID_NAROCILO = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    ID_STRANKA = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    ZAHTEVANA_KOLICINA = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    DATUM_NAROCILA = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NAROCILO", x => x.ID_NAROCILO)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_NAROCILO_JE_NAROCI_STRANKA",
                        column: x => x.ID_STRANKA,
                        principalTable: "STRANKA",
                        principalColumn: "ID_STRANKA");
                });

            migrationBuilder.CreateTable(
                name: "TERMIN",
                columns: table => new
                {
                    ID_VETERINAR = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    DATUM_ZACETKA = table.Column<DateTime>(type: "datetime", nullable: false),
                    DATUM_KONCA = table.Column<DateTime>(type: "datetime", nullable: false),
                    ID_STRANKA = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    JE_ZASEDEN = table.Column<bool>(type: "bit", nullable: false),
                    JE_POTRJEN = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TERMIN", x => new { x.ID_VETERINAR, x.DATUM_ZACETKA, x.DATUM_KONCA })
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_TERMIN_JE_PREVZE_STRANKA",
                        column: x => x.ID_STRANKA,
                        principalTable: "STRANKA",
                        principalColumn: "ID_STRANKA");
                    table.ForeignKey(
                        name: "FK_TERMIN_JE_RAZPIS_VETERINA",
                        column: x => x.ID_VETERINAR,
                        principalTable: "VETERINAR",
                        principalColumn: "ID_VETERINAR");
                });

            migrationBuilder.CreateTable(
                name: "ZALOGA",
                columns: table => new
                {
                    ID_IZDELEK = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    ID_VETERINAR = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    KOLICINA = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZALOGA", x => new { x.ID_IZDELEK, x.ID_VETERINAR });
                    table.ForeignKey(
                        name: "FK_ZALOGA_IMA_VETERINA",
                        column: x => x.ID_VETERINAR,
                        principalTable: "VETERINAR",
                        principalColumn: "ID_VETERINAR");
                    table.ForeignKey(
                        name: "FK_ZALOGA_JE_OD_IZDELEK",
                        column: x => x.ID_IZDELEK,
                        principalTable: "IZDELEK",
                        principalColumn: "ID_IZDELEK");
                });

            migrationBuilder.CreateTable(
                name: "EVIDENCA",
                columns: table => new
                {
                    ID_EVIDENCE = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    ID_VETERINAR = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    DATUM_ZACETKA = table.Column<DateTime>(type: "datetime", nullable: true),
                    DATUM_KONCA = table.Column<DateTime>(type: "datetime", nullable: true),
                    ID_NAROCILO = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    CENA = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVIDENCA", x => x.ID_EVIDENCE)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_EVIDENCA_JE_BILO_O_NAROCILO",
                        column: x => x.ID_NAROCILO,
                        principalTable: "NAROCILO",
                        principalColumn: "ID_NAROCILO");
                    table.ForeignKey(
                        name: "FK_EVIDENCA_JE_ZABELE_TERMIN",
                        columns: x => new { x.ID_VETERINAR, x.DATUM_ZACETKA, x.DATUM_KONCA },
                        principalTable: "TERMIN",
                        principalColumns: new[] { "ID_VETERINAR", "DATUM_ZACETKA", "DATUM_KONCA" });
                });

            migrationBuilder.CreateTable(
                name: "ZAHTEVA",
                columns: table => new
                {
                    ID_IZDELEK = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    ID_NAROCILO = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    ID_VETERINAR = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZAHTEVA", x => new { x.ID_IZDELEK, x.ID_NAROCILO, x.ID_VETERINAR });
                    table.ForeignKey(
                        name: "FK_ZAHTEVA_ZAHTEVA_NAROCILO",
                        column: x => x.ID_NAROCILO,
                        principalTable: "NAROCILO",
                        principalColumn: "ID_NAROCILO");
                    table.ForeignKey(
                        name: "FK_ZAHTEVA_ZAHTEVA2_ZALOGA",
                        columns: x => new { x.ID_IZDELEK, x.ID_VETERINAR },
                        principalTable: "ZALOGA",
                        principalColumns: new[] { "ID_IZDELEK", "ID_VETERINAR" });
                });

            migrationBuilder.CreateTable(
                name: "JE_BILA_OPRAVLJENA",
                columns: table => new
                {
                    ID_STORITEV = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    ID_EVIDENCE = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JE_BILA_OPRAVLJENA", x => new { x.ID_STORITEV, x.ID_EVIDENCE });
                    table.ForeignKey(
                        name: "FK_JE_BILA__JE_BILA_O_EVIDENCA",
                        column: x => x.ID_EVIDENCE,
                        principalTable: "EVIDENCA",
                        principalColumn: "ID_EVIDENCE");
                    table.ForeignKey(
                        name: "FK_JE_BILA__JE_BILA_O_STORITEV",
                        column: x => x.ID_STORITEV,
                        principalTable: "STORITEV",
                        principalColumn: "ID_STORITEV");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "JE_BILO_OPRAVLJENO_FK",
                table: "EVIDENCA",
                column: "ID_NAROCILO");

            migrationBuilder.CreateIndex(
                name: "JE_ZABELEZENO_V_FK",
                table: "EVIDENCA",
                columns: new[] { "ID_VETERINAR", "DATUM_ZACETKA", "DATUM_KONCA" });

            migrationBuilder.CreateIndex(
                name: "JE_BILA_OPRAVLJENA_FK",
                table: "JE_BILA_OPRAVLJENA",
                column: "ID_STORITEV");

            migrationBuilder.CreateIndex(
                name: "JE_BILA_OPRAVLJENA2_FK",
                table: "JE_BILA_OPRAVLJENA",
                column: "ID_EVIDENCE");

            migrationBuilder.CreateIndex(
                name: "JE_NAROCILA_FK",
                table: "NAROCILO",
                column: "ID_STRANKA");

            migrationBuilder.CreateIndex(
                name: "IX_STRANKA_AspNetIDId",
                table: "STRANKA",
                column: "AspNetIDId");

            migrationBuilder.CreateIndex(
                name: "JE_NA_FK",
                table: "STRANKA",
                column: "STEVILKA");

            migrationBuilder.CreateIndex(
                name: "JE_PREVZELA_FK",
                table: "TERMIN",
                column: "ID_STRANKA");

            migrationBuilder.CreateIndex(
                name: "JE_RAZPISAL_FK",
                table: "TERMIN",
                column: "ID_VETERINAR");

            migrationBuilder.CreateIndex(
                name: "IMA_VETERINO_NA_FK",
                table: "VETERINAR",
                column: "STEVILKA");

            migrationBuilder.CreateIndex(
                name: "IX_VETERINAR_AspNetIDId",
                table: "VETERINAR",
                column: "AspNetIDId");

            migrationBuilder.CreateIndex(
                name: "ZAHTEVA_FK",
                table: "ZAHTEVA",
                column: "ID_NAROCILO");

            migrationBuilder.CreateIndex(
                name: "ZAHTEVA2_FK",
                table: "ZAHTEVA",
                columns: new[] { "ID_IZDELEK", "ID_VETERINAR" });

            migrationBuilder.CreateIndex(
                name: "IMA_FK",
                table: "ZALOGA",
                column: "ID_VETERINAR");

            migrationBuilder.CreateIndex(
                name: "JE_OD_FK",
                table: "ZALOGA",
                column: "ID_IZDELEK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "JE_BILA_OPRAVLJENA");

            migrationBuilder.DropTable(
                name: "ZAHTEVA");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EVIDENCA");

            migrationBuilder.DropTable(
                name: "STORITEV");

            migrationBuilder.DropTable(
                name: "ZALOGA");

            migrationBuilder.DropTable(
                name: "NAROCILO");

            migrationBuilder.DropTable(
                name: "TERMIN");

            migrationBuilder.DropTable(
                name: "IZDELEK");

            migrationBuilder.DropTable(
                name: "STRANKA");

            migrationBuilder.DropTable(
                name: "VETERINAR");

            migrationBuilder.DropTable(
                name: "POSTA");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
