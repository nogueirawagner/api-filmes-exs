using Exs.Domain.Entities;
using Exs.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exs.Infra.Data.Mappings
{
  public class FilmeMap : EntityTypeConfiguration<Filme>
  {
    public override void Map(EntityTypeBuilder<Filme> builder)
    {
      builder.ToTable("Filme");

      builder.HasKey(s => s.Id);

      builder.Property(s => s.Nome)
        .HasColumnType("nvarchar(200)")
        .IsRequired();

      builder.Property(s => s.DataCriacao)
        .HasColumnType("datetime2");

      builder.Property(s => s.Ativo)
        .HasColumnType("bit");

      builder.HasOne(s => s.Genero)
        .WithMany(s => s.Filmes)
        .HasForeignKey(s => s.GeneroId)
        .IsRequired();

      builder.Ignore(e => e.ValidationResult);
      builder.Ignore(e => e.ValidationErrors);
      builder.Ignore(s => s.CascadeMode);
    }
  }
}
