using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Exs.Domain.Entities;
using Exs.Domain.Interfaces;
using Exs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Exs.Infra.Data.Repository
{
  public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
  {
    protected ContextDb Db;
    protected DbSet<TEntity> DbSet;

    protected Repository(ContextDb context)
    {
      Db = context;
      DbSet = Db.Set<TEntity>();
    }

    public void Adicionar(TEntity obj)
    {
      DbSet.Add(obj);
    }

    public void Atualizar(TEntity obj)
    {
      DbSet.Update(obj);
    }

    public TEntity PegarPorId(int id)
    {
      return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
    }

    public virtual IEnumerable<TEntity> PegarTodos()
    {
      return DbSet.AsNoTracking().ToList();
    }

    public virtual IEnumerable<TEntity> Pegar(Expression<Func<TEntity, bool>> predicate)
    {
      return DbSet.AsNoTracking().Where(predicate);
    }

    public void Remover(int id)
    {
      DbSet.Remove(DbSet.Find(id));
    }

    public void Remover(IEnumerable<TEntity> entities)
    {
      DbSet.RemoveRange(entities);
    }

    public void Remover(List<int> ids)
    {
      var lst = new List<TEntity>();
      ids.ForEach(p => lst.Add(PegarPorId(p)));
      DbSet.RemoveRange(lst);
    }

    public int SaveChanges()
    {
      return Db.SaveChanges();
    }

    public void Dispose()
    {
      Db.Dispose();
    }
  }
}
