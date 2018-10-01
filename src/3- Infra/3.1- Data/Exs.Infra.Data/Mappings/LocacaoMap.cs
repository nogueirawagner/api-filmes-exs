using Exs.Domain.Entities;
using Exs.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exs.Infra.Data.Mappings
{
  public class LocacaoMap : EntityTypeConfiguration<Locacao>
  {
    public override void Map(EntityTypeBuilder<Locacao> builder)
    {
      builder.ToTable("Locacao");

      builder.HasKey(s => s.Id);

      builder.Property(s => s.CPF)
        .HasColumnType("nvarchar(14)")
        .HasMaxLength(14)
        .IsRequired();

      builder.Property(s => s.DataLocacao)
        .HasColumnType("datetime2")
        .IsRequired();

      builder.Ignore(s => s.ValidationResult);
      builder.Ignore(e => e.ValidationErrors);
      builder.Ignore(s => s.CascadeMode);
    }
  }
}
