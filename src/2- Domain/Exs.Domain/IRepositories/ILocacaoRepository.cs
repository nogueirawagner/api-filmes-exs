using Exs.Domain.Entities;
using Exs.Domain.Interfaces;
using System.Collections.Generic;

namespace Exs.Domain.IRepositories
{
  public interface ILocacaoRepository : IRepository<Locacao>
  {
    IEnumerable<Locacao> PegarTodos(string cpf);
  }
}
