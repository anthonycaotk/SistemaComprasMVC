using Microsoft.EntityFrameworkCore;
using SistemaComprasMVC.Models;

namespace SistemaComprasMVC.Data
{
    public class SistemaComprasContext : DbContext
    {
        public SistemaComprasContext(DbContextOptions<SistemaComprasContext> options)
            : base(options)
        {
        }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<UnidadDeMedida> UnidadesDeMedida { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        // Eliminar la propiedad DbSet<OrdenDeCompra>
        // public DbSet<OrdenDeCompra> OrdenesDeCompra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Eliminar la configuración relacionada con OrdenDeCompra
            // modelBuilder.Entity<OrdenDeCompra>()
            //     .HasOne(o => o.Articulo)
            //     .WithMany()
            //     .HasForeignKey(o => o.ArticuloId)
            //     .OnDelete(DeleteBehavior.Restrict); // Cambia a Restrict

            // modelBuilder.Entity<OrdenDeCompra>()
            //     .HasOne(o => o.UnidadDeMedida)
            //     .WithMany()
            //     .HasForeignKey(o => o.UnidadDeMedidaId)
            //     .OnDelete(DeleteBehavior.Restrict); // Cambia a Restrict

            modelBuilder.Entity<Articulo>()
                .HasOne(a => a.UnidadDeMedida)
                .WithMany()
                .HasForeignKey(a => a.UnidadDeMedidaId)
                .OnDelete(DeleteBehavior.Restrict); // Cambia a Restrict para evitar ciclos

            base.OnModelCreating(modelBuilder);
        }
    }
}
