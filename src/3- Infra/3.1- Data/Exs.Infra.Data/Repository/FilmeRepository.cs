using Dapper;
using Exs.Domain.Entities;
using Exs.Domain.IRepositories;
using Exs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Exs.Infra.Data.Repository
{
  public class FilmeRepository : Repository<Filme>, IFilmeRepository
  {
    public FilmeRepository(ContextDb context)
    : base(context)
    {
    }

    // Pega todos os filmes utilizando dapper.
    // Dapper tem melhor desempenho para leitura do que para leitura.
    public override IEnumerable<Filme> PegarTodos()
    {
      var sql = @"
                SELECT *
                   FROM filme f
                WHERE f.Ativo = 1
                ORDER BY f.nome;";

      return Db.Database.GetDbConnection().Query<Filme>(sql).ToList();
    }
  }
}