using FluentValidation;
using System;
using System.Collections.Generic;

namespace Exs.Domain.Entities
{
  public class Locacao : Entity<Locacao>
  {
    public Locacao()
    {
      DataLocacao = DateTime.Now;
    }

    public string CPF { get; set; }
    public DateTime DataLocacao { get; set; }

    public virtual ICollection<LocacaoFilme> Locacoes { get; set; }

    private void Validar()
    {
      RuleFor(s => s.Locacoes)
        .Must(collection => collection == null)
        .WithMessage("A locação deve conter filmes.");

      ValidationResult = Validate(this);
    }

    public override bool EhValido()
    {
      Validar();
      foreach (var erro in ValidationResult.Errors)
        ValidationErrors.Add(erro.ErrorMessage);
      return ValidationResult.IsValid;
    }
  }
}
