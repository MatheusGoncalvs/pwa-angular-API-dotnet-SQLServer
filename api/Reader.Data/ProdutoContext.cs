using Microsoft.EntityFrameworkCore;
using Reader.Domain;

namespace Reader.Data
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext()
        {
        }
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options)
        { }
        public DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<Lote> Lotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            builder.Entity<Lote>(entity =>
            {
                entity.ToTable("Lote");

                entity.Property(e => e.DataImportacao).HasColumnType("date");

                entity.Property(e => e.MenorDataEntrega).HasColumnType("date");
            });

            builder.Entity<Produto>(entity =>
            {
                entity.ToTable("Produto");

                entity.Property(e => e.DataEntrega).HasColumnType("date");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValorUnitario).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdLoteNavigation)
                    .WithMany(p => p.Produtos)
                    .HasForeignKey(d => d.IdLote)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lote_Produto");
            });
        }
    }
}