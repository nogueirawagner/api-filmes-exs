using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Exs.Domain.Entities
{
  /// <summary>
  /// Classe abstrata, poderá conter métodos para validação do próprio objeto utilizando FluentValidator.
  /// Fazendo com que o objeto seja auto validado, não sendo necessário fazer validações em camadas inferiores.
  /// Este padrão é utilizado muito em abordagens CQRS.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
  {
    public Entity()
    {
      ValidationResult = new ValidationResult();
      ValidationErrors = new List<string>();
    }

    public int Id { get; set; }

    public abstract bool EhValido();
    public ValidationResult ValidationResult { get; protected set; }
    public List<string> ValidationErrors { get; protected set; }
  }
}
