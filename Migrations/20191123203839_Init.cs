using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BE.Example.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 4, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleId);
                });

            migrationBuilder.CreateTable(
                name: "Literals",
                columns: table => new
                {
                    LiteralId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Plural = table.Column<bool>(nullable: false),
                    ExampleURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Literals", x => x.LiteralId);
                    table.ForeignKey(
                        name: "FK_Literals_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiteralTranslations",
                columns: table => new
                {
                    LiteralTranslationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: true),
                    LiteralId = table.Column<int>(nullable: false),
                    ValueZero = table.Column<string>(nullable: true),
                    ValueOne = table.Column<string>(nullable: true),
                    ValueMany = table.Column<string>(nullable: true),
                    InReview = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteralTranslations", x => x.LiteralTranslationId);
                    table.ForeignKey(
                        name: "FK_LiteralTranslations_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiteralTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiteralTranslations_Literals_LiteralId",
                        column: x => x.LiteralId,
                        principalTable: "Literals",
                        principalColumn: "LiteralId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    VariableId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LiteralId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.VariableId);
                    table.ForeignKey(
                        name: "FK_Variables_Literals_LiteralId",
                        column: x => x.LiteralId,
                        principalTable: "Literals",
                        principalColumn: "LiteralId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AR", "Argentina" },
                    { 2, "BR", "Brazil" },
                    { 3, "MX", "Mexico" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "Code", "Name" },
                values: new object[] { 1, "es", "Spanish; Castilian" });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "ModuleId", "Name" },
                values: new object[] { 1, "Login & Registration" });

            migrationBuilder.InsertData(
                table: "Literals",
                columns: new[] { "LiteralId", "Code", "Description", "ExampleURL", "ModuleId", "Plural" },
                values: new object[] { 1, "login_button", "The label of the login button", "/login#login_button", 1, false });

            migrationBuilder.InsertData(
                table: "LiteralTranslations",
                columns: new[] { "LiteralTranslationId", "CountryId", "InReview", "LanguageId", "LiteralId", "ValueMany", "ValueOne", "ValueZero" },
                values: new object[] { 1, null, false, 1, 1, "Se encontraron %quantity resultados.", "Se encontró un resultado", "No se encontraron resultados." });

            migrationBuilder.InsertData(
                table: "Variables",
                columns: new[] { "VariableId", "LiteralId", "Name" },
                values: new object[] { 1, 1, "%quantity" });

            migrationBuilder.CreateIndex(
                name: "IX_Literals_ModuleId",
                table: "Literals",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteralTranslations_CountryId",
                table: "LiteralTranslations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteralTranslations_LanguageId",
                table: "LiteralTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteralTranslations_LiteralId",
                table: "LiteralTranslations",
                column: "LiteralId");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_LiteralId",
                table: "Variables",
                column: "LiteralId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiteralTranslations");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Literals");

            migrationBuilder.DropTable(
                name: "Modules");
        }
    }
}
