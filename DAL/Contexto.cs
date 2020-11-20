using System;
using System.Collections.Generic;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Cobros> Cobros { get; set; }
        public DbSet<Creditos> Creditos { get; set; }
        public DbSet<EmpleadosVentas> EmpleadosVentas { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<TiposProducto> TiposProductos { get; set; }
        public DbSet<UsoProducto> UsoProducto { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<VentasContado> VentasContados { get; set; }
        public DbSet<VentasCredito> VentasCreditos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source = DATA\RepuestosAntiguaDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsoProducto>().HasData(new List<UsoProducto>() 
            { 
                new UsoProducto(1, "Nuevo"),
                new UsoProducto(2, "Usado")
            });
        }
    }
}
