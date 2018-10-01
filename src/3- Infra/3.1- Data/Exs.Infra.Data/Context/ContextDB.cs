using Exs.Domain.Entities;
using Exs.Infra.Data.Extensions;
using Exs.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Exs.Infra.Data.Context
{
  public class ContextDb : DbContext
  {
    public DbSet<Filme> Filme { get; set; }
    public DbSet<Genero> Genero { get; set; }
    public DbSet<Locacao> Locacao { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddConfiguration(new GeneroMap());
      modelBuilder.AddConfiguration(new FilmeMap());
      modelBuilder.AddConfiguration(new LocacaoMap());
      modelBuilder.AddConfiguration(new LocacaoFilmeMap());

      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      optionsBuilder.UseSqlServer(config.GetConnectionString("AWS"));
    }
  }
}
