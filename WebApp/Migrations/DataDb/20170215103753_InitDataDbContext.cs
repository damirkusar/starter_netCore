using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations.DataDb
{
    public partial class InitDataDbContext : Migration
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
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Container = table.Column<string>(type: "varchar(255)", nullable: true),
                    Key = table.Column<string>(type: "varchar(255)", nullable: false),
                    Language = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localisations", x => x.Id);
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
