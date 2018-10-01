using Exs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Exs.Domain.Interfaces
{
  public interface IRepository<TEntity> where TEntity : Entity<TEntity>
  {
    void Adicionar(TEntity obj);
    void Atualizar(TEntity obj);
    TEntity PegarPorId(int id);
    IEnumerable<TEntity> PegarTodos();
    IEnumerable<TEntity> Pegar(Expression<Func<TEntity, bool>> predicate);
    void Remover(int id);
    void Remover(List<int> ids);
    int SaveChanges();
    void Dispose();
  }
}
