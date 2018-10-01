using Exs.Domain.Entities;
using Exs.Domain.Interfaces;
using Exs.Infra.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exs.Domain.IRepositories
{
  public interface ILocacaoFilmeRepository : IRepository<LocacaoFilme>
  {
    IEnumerable<IGrouping<int, LocacaoFilme>> PegarLocacoesFilme(string cpf);
    IEnumerable<IGrouping<int, LocacaoFilme>> PegarLocacoesFilmePorLocacaoId(int locacaoId, string cpf);
  }
}
