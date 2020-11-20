﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SistemaRepuestosAntigua_AP1_PF.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Entidades.Clientes", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("NoCedula")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Rnc")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Entidades.Cobros", b =>
                {
                    b.Property<int>("CobroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreditoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmpleadoVentasId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Total")
                        .HasColumnType("Money");

                    b.HasKey("CobroId");

                    b.ToTable("Cobros");
                });

            modelBuilder.Entity("Entidades.CobrosDetalle", b =>
                {
                    b.Property<int>("CobroDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CobroId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Monto")
                        .HasColumnType("TEXT");

                    b.Property<int>("VentaCreditoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CobroDetalleId");

                    b.HasIndex("CobroId");

                    b.ToTable("CobrosDetalle");
                });

            modelBuilder.Entity("Entidades.Creditos", b =>
                {
                    b.Property<int>("CreditoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .HasColumnType("Money");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Monto")
                        .HasColumnType("Money");

                    b.HasKey("CreditoId");

                    b.ToTable("Creditos");
                });

            modelBuilder.Entity("Entidades.EmpleadosVentas", b =>
                {
                    b.Property<int>("EmpleadoVentasId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmpleadoVentasId");

                    b.ToTable("EmpleadosVentas");
                });

            modelBuilder.Entity("Entidades.Productos", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<float>("Descuento")
                        .HasColumnType("REAL");

                    b.Property<float>("Existencia")
                        .HasColumnType("REAL");

                    b.Property<decimal>("PrecioUnit")
                        .HasColumnType("Money");

                    b.Property<int>("TipoProductoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsoProductoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Entidades.TiposProducto", b =>
                {
                    b.Property<int>("TipoProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("TipoProductoId");

                    b.ToTable("TiposProductos");
                });

            modelBuilder.Entity("Entidades.UsoProducto", b =>
                {
                    b.Property<int>("UsoProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("UsoProductoId");

                    b.ToTable("UsoProducto");

                    b.HasData(
                        new
                        {
                            UsoProductoId = 1,
                            Descripcion = "Nuevo"
                        },
                        new
                        {
                            UsoProductoId = 2,
                            Descripcion = "Usado"
                        });
                });

            modelBuilder.Entity("Entidades.Usuarios", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Entidades.VentasContado", b =>
                {
                    b.Property<int>("VentaContadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Condicion")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Itbis")
                        .HasColumnType("Money");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("Money");

                    b.Property<decimal>("Total")
                        .HasColumnType("Money");

                    b.HasKey("VentaContadoId");

                    b.ToTable("VentasContados");
                });

            modelBuilder.Entity("Entidades.VentasContadoDetalle", b =>
                {
                    b.Property<int>("DetalleVentaContadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Cantidad")
                        .HasColumnType("REAL");

                    b.Property<int>("ProductoId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Total")
                        .HasColumnType("Money");

                    b.Property<int>("VentaContadoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DetalleVentaContadoId");

                    b.HasIndex("VentaContadoId");

                    b.ToTable("VentasContadoDetalle");
                });

            modelBuilder.Entity("Entidades.VentasCredito", b =>
                {
                    b.Property<int>("VentaCreditoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Itbis")
                        .HasColumnType("Money");

                    b.Property<string>("Ncf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.Property<string>("NoAutorizacion")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("Money");

                    b.Property<decimal>("Total")
                        .HasColumnType("Money");

                    b.HasKey("VentaCreditoId");

                    b.ToTable("VentasCreditos");
                });

            modelBuilder.Entity("Entidades.VentasCreditoDetalle", b =>
                {
                    b.Property<int>("DetalleVentaCreditoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Cantidad")
                        .HasColumnType("REAL");

                    b.Property<int>("ProductoId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Total")
                        .HasColumnType("Money");

                    b.Property<int>("VentaCreditoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DetalleVentaCreditoId");

                    b.HasIndex("VentaCreditoId");

                    b.ToTable("VentasCreditoDetalle");
                });

            modelBuilder.Entity("Entidades.CobrosDetalle", b =>
                {
                    b.HasOne("Entidades.Cobros", null)
                        .WithMany("DetalleCobro")
                        .HasForeignKey("CobroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entidades.VentasContadoDetalle", b =>
                {
                    b.HasOne("Entidades.VentasContado", null)
                        .WithMany("DetalleVentaContado")
                        .HasForeignKey("VentaContadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entidades.VentasCreditoDetalle", b =>
                {
                    b.HasOne("Entidades.VentasCredito", null)
                        .WithMany("DetalleVentaCredito")
                        .HasForeignKey("VentaCreditoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entidades.Cobros", b =>
                {
                    b.Navigation("DetalleCobro");
                });

            modelBuilder.Entity("Entidades.VentasContado", b =>
                {
                    b.Navigation("DetalleVentaContado");
                });

            modelBuilder.Entity("Entidades.VentasCredito", b =>
                {
                    b.Navigation("DetalleVentaCredito");
                });
#pragma warning restore 612, 618
        }
    }
}
