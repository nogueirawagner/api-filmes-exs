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
  }
}