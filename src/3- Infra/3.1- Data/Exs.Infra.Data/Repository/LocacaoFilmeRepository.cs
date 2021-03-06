﻿using Dapper;
using Exs.Domain.Entities;
using Exs.Domain.IRepositories;
using Exs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exs.Infra.Data.Repository
{
  public class LocacaoFilmeRepository : Repository<LocacaoFilme>, ILocacaoFilmeRepository
  {
    public LocacaoFilmeRepository(ContextDb context)
      : base(context)
    {
    }

    // Pegando os filmes feitos pelas locações utilizando dapper.
    public IEnumerable<IGrouping<int, LocacaoFilme>> PegarLocacoesFilme(string cpf)
    {
      var sql = @"
                select * 
                   from Locacao l
                    join LocacaoFilme lf on lf.LocacaoId = l.Id
                    join Filme f on lf.FilmeId = f.Id
                  where l.CPF = @cpf";

      return Db.Database.GetDbConnection().Query<Locacao, LocacaoFilme, Filme, LocacaoFilme>(sql
        , (locacao, locacaoFilme, filme) =>
         {
           if (locacao.Locacoes == null)
             locacao.Locacoes = new List<LocacaoFilme>();

           if (locacaoFilme.Filme == null)
             locacaoFilme.Filme = filme;

           return locacaoFilme;
         }, new { cpf }).GroupBy(s => s.LocacaoId);
    }

    // Pegando os filmes feitos pelas locaçao utilizando dapper.
    public IEnumerable<IGrouping<int, LocacaoFilme>> PegarLocacoesFilmePorLocacaoId(int locacaoId, string cpf)
    {
      var sql = @"
                select * 
                   from Locacao l
                    join LocacaoFilme lf on lf.LocacaoId = l.Id
                    join Filme f on lf.FilmeId = f.Id
                  where l.id = @locacaoId and l.CPF = @cpf";

      return Db.Database.GetDbConnection().Query<Locacao, LocacaoFilme, Filme, LocacaoFilme>(sql
        , (locacao, locacaoFilme, filme) =>
        {
          if (locacao.Locacoes == null)
            locacao.Locacoes = new List<LocacaoFilme>();

          if (locacaoFilme.Filme == null)
            locacaoFilme.Filme = filme;

          return locacaoFilme;
        }, new { locacaoId, cpf }).GroupBy(s => s.LocacaoId);
    }
  }
}
