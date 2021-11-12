using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class MigracionDeEntidadesConLlavesForaneas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleFacturaProductos_Productos_ProductoId",
                table: "DetalleFacturaProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_CategoriasProductos_CategoriaProductoId",
                table: "Productos");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaProductoId",
                table: "Productos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductoId",
                table: "DetalleFacturaProductos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleFacturaProductos_Productos_ProductoId",
                table: "DetalleFacturaProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_CategoriasProductos_CategoriaProductoId",
                table: "Productos",
                column: "CategoriaProductoId",
                principalTable: "CategoriasProductos",
                principalColumn: "CategoriaProductoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleFacturaProductos_Productos_ProductoId",
                table: "DetalleFacturaProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_CategoriasProductos_CategoriaProductoId",
                table: "Productos");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaProductoId",
                table: "Productos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductoId",
                table: "DetalleFacturaProductos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleFacturaProductos_Productos_ProductoId",
                table: "DetalleFacturaProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_CategoriasProductos_CategoriaProductoId",
                table: "Productos",
                column: "CategoriaProductoId",
                principalTable: "CategoriasProductos",
                principalColumn: "CategoriaProductoId");
        }
    }
}
