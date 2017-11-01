using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp.Localisation.DataAccessLayer.Migrations
{
    public partial class InitLocalisationDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Model");

            migrationBuilder.CreateTable(
                name: "Localisations",
                schema: "Model",
                columns: table => new
                {
                    LocalisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageIsoAlpha2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localisations", x => x.LocalisationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localisations",
                schema: "Model");
        }
    }
}
