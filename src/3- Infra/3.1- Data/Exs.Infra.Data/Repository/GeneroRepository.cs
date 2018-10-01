using Dapper;
using Exs.Domain.Entities;
using Exs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Exs.Domain.IRepositories;

namespace Exs.Infra.Data.Repository
{
  public class GeneroRepository : Repository<Genero>, IGeneroRepository
  {
    public GeneroRepository(ContextDb context)
    : base(context)
    {
    }

    // Utilizando dapper para leitura.
    public override IEnumerable<Genero> PegarTodos()
    {
      var sql = @"
                SELECT *
                   FROM genero g
                WHERE g.Ativo = 1
                ORDER BY g.nome;";

      return Db.Database.GetDbConnection().Query<Genero>(sql).ToList();
    }
  }
}