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
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false)
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
                    CreditoId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpleadoVentasId = table.Column<int>(type: "INTEGER", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobros", x => x.CobroId);
                });

            migrationBuilder.CreateTable(
                name: "Creditos",
                columns: table => new
                {
                    CreditoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "Money", nullable: false),
                    Balance = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creditos", x => x.CreditoId);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadosVentas",
                columns: table => new
                {
                    EmpleadoVentasId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Nombres = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Apellidos = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadosVentas", x => x.EmpleadoVentasId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PrecioUnit = table.Column<decimal>(type: "Money", nullable: false),
                    Descuento = table.Column<float>(type: "REAL", nullable: false),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    TipoProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsoProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Existencia = table.Column<float>(type: "REAL", nullable: false)
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
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposProductos", x => x.TipoProductoId);
                });

            migrationBuilder.CreateTable(
                name: "UsoProducto",
                columns: table => new
                {
                    UsoProductoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsoProducto", x => x.UsoProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreUsuario = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    Clave = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "VentasContados",
                columns: table => new
                {
                    VentaContadoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Condicion = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    SubTotal = table.Column<decimal>(type: "Money", nullable: false),
                    Itbis = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasContados", x => x.VentaContadoId);
                });

            migrationBuilder.CreateTable(
                name: "VentasCreditos",
                columns: table => new
                {
                    VentaCreditoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NoAutorizacion = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Ncf = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    SubTotal = table.Column<decimal>(type: "Money", nullable: false),
                    Itbis = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasCreditos", x => x.VentaCreditoId);
                });

            migrationBuilder.CreateTable(
                name: "CobrosDetalle",
                columns: table => new
                {
                    CobroDetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CobroId = table.Column<int>(type: "INTEGER", nullable: false),
                    VentaCreditoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false)
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
                name: "VentasContadoDetalle",
                columns: table => new
                {
                    DetalleVentaContadoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VentaContadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<float>(type: "REAL", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasContadoDetalle", x => x.DetalleVentaContadoId);
                    table.ForeignKey(
                        name: "FK_VentasContadoDetalle_VentasContados_VentaContadoId",
                        column: x => x.VentaContadoId,
                        principalTable: "VentasContados",
                        principalColumn: "VentaContadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentasCreditoDetalle",
                columns: table => new
                {
                    DetalleVentaCreditoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VentaCreditoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<float>(type: "REAL", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasCreditoDetalle", x => x.DetalleVentaCreditoId);
                    table.ForeignKey(
                        name: "FK_VentasCreditoDetalle_VentasCreditos_VentaCreditoId",
                        column: x => x.VentaCreditoId,
                        principalTable: "VentasCreditos",
                        principalColumn: "VentaCreditoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UsoProducto",
                columns: new[] { "UsoProductoId", "Descripcion" },
                values: new object[] { 1, "Nuevo" });

            migrationBuilder.InsertData(
                table: "UsoProducto",
                columns: new[] { "UsoProductoId", "Descripcion" },
                values: new object[] { 2, "Usado" });

            migrationBuilder.CreateIndex(
                name: "IX_CobrosDetalle_CobroId",
                table: "CobrosDetalle",
                column: "CobroId");

            migrationBuilder.CreateIndex(
                name: "IX_VentasContadoDetalle_VentaContadoId",
                table: "VentasContadoDetalle",
                column: "VentaContadoId");

            migrationBuilder.CreateIndex(
                name: "IX_VentasCreditoDetalle_VentaCreditoId",
                table: "VentasCreditoDetalle",
                column: "VentaCreditoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "CobrosDetalle");

            migrationBuilder.DropTable(
                name: "Creditos");

            migrationBuilder.DropTable(
                name: "EmpleadosVentas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "TiposProductos");

            migrationBuilder.DropTable(
                name: "UsoProducto");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "VentasContadoDetalle");

            migrationBuilder.DropTable(
                name: "VentasCreditoDetalle");

            migrationBuilder.DropTable(
                name: "Cobros");

            migrationBuilder.DropTable(
                name: "VentasContados");

            migrationBuilder.DropTable(
                name: "VentasCreditos");
        }
    }
}
