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
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<TiposProducto> TiposProductos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Ventas> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source = DATA\RepuestosAntiguaDB.db");
        }
    }
}
