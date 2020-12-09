using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaRepuestosAntigua_AP1_PF.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NoCedula = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Rnc = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Nombres = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Apellidos = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    UsuarioModificador = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Cobros",
                columns: table => new
                {
                    CobroId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<float>(type: "Money", nullable: false),
                    UsuarioModificador = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobros", x => x.CobroId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PrecioUnit = table.Column<float>(type: "Money", nullable: false),
                    Descuento = table.Column<float>(type: "REAL", nullable: false),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    TipoProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    EstadoProducto = table.Column<int>(type: "Bit", nullable: false),
                    Existencia = table.Column<float>(type: "REAL", nullable: false),
                    Impuesto = table.Column<float>(type: "REAL", nullable: false),
                    UsuarioModificador = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "TiposProductos",
                columns: table => new
                {
                    TipoProductoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    UsuarioModificador = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProductos", x => x.TipoProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombres = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Apellidos = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NombreUsuario = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    Clave = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    UsuarioModificador = table.Column<int>(type: "INTEGER", nullable: false),
                    EsAdmin = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    VentaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoVenta = table.Column<int>(type: "Bit", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ncf = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    Itbis = table.Column<float>(type: "Money", nullable: false),
                    Total = table.Column<float>(type: "Money", nullable: false),
                    UsuarioModificador = table.Column<int>(type: "INTEGER", nullable: false),
                    PendientePagar = table.Column<float>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.VentaId);
                });

            migrationBuilder.CreateTable(
                name: "CobrosDetalle",
                columns: table => new
                {
                    CobroDetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CobroId = table.Column<int>(type: "INTEGER", nullable: false),
                    VentaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Monto = table.Column<float>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobrosDetalle", x => x.CobroDetalleId);
                    table.ForeignKey(
                        name: "FK_CobrosDetalle_Cobros_CobroId",
                        column: x => x.CobroId,
                        principalTable: "Cobros",
                        principalColumn: "CobroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentasDetalle",
                columns: table => new
                {
                    DetalleVentaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VentaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<float>(type: "REAL", nullable: false),
                    Total = table.Column<float>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasDetalle", x => x.DetalleVentaId);
                    table.ForeignKey(
                        name: "FK_VentasDetalle_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "VentaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CobrosDetalle_CobroId",
                table: "CobrosDetalle",
                column: "CobroId");

            migrationBuilder.CreateIndex(
                name: "IX_VentasDetalle_VentaId",
                table: "VentasDetalle",
                column: "VentaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "CobrosDetalle");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "TiposProductos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "VentasDetalle");

            migrationBuilder.DropTable(
                name: "Cobros");

            migrationBuilder.DropTable(
                name: "Ventas");
        }
    }
}
