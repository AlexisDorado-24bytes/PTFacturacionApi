﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class NoCodigouniqqqqqq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoriasProductos_Codigo",
                table: "CategoriasProductos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CategoriasProductos_Codigo",
                table: "CategoriasProductos",
                column: "Codigo",
                unique: true);
        }
    }
}
