using Exs.Domain.Entities;
using Exs.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exs.Infra.Data.Mappings
{
  public class LocacaoFilmeMap : EntityTypeConfiguration<LocacaoFilme>
  {
    public override void Map(EntityTypeBuilder<LocacaoFilme> builder)
    {
      builder.ToTable("LocacaoFilme");

      builder.Property(s => s.FilmeId)
        .HasColumnType("int");

      builder.Property(s => s.LocacaoId)
        .HasColumnType("int");

      builder.HasOne(p => p.Filme)
          .WithMany(p => p.Locacoes)
          .HasForeignKey(p => p.FilmeId);

      builder.HasOne(p => p.Locacao)
          .WithMany(p => p.Locacoes)
          .HasForeignKey(p => p.LocacaoId);

      builder.Ignore(s => s.ValidationResult);
      builder.Ignore(e => e.ValidationErrors);
      builder.Ignore(s => s.CascadeMode);
    }
  }
}
