using Dapper;
using Exs.Domain.Entities;
using Exs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Exs.Domain.IRepositories;

namespace Exs.Infra.Data.Repository
{
  public class LocacaoRepository : Repository<Locacao>, ILocacaoRepository
  {
    public LocacaoRepository(ContextDb context)
      : base(context)
    {
    }


    // Pega todos os filmes utilizando dapper.
    // Dapper tem melhor desempenho para leitura do que para leitura.

    public IEnumerable<Locacao> PegarTodos(string cpf)
    {
      var sql = @"
                SELECT *
                   FROM Locacao l
                WHERE l.CPF = @cpf
                ORDER BY l.DataLocacao;";

      return Db.Database.GetDbConnection().Query<Locacao>(sql, new { cpf }).ToList();
    }
  }
}