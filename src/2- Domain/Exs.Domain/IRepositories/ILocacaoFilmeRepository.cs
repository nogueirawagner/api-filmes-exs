using Exs.Domain.Entities;
using Exs.Domain.Interfaces;
using Exs.Infra.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Exs.Domain.IRepositories
{
  public interface ILocacaoFilmeRepository : IRepository<LocacaoFilme>
  {
    IEnumerable<IGrouping<int, LocacaoFilme>> PegarLocacoesFilme();
    IEnumerable<IGrouping<int, LocacaoFilme>> PegarLocacoesFilmePorLocacaoId(int locacaoId);
  }
}
