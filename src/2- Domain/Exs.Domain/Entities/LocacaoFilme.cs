using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exs.Domain.Entities
{
  public class LocacaoFilme : Entity<LocacaoFilme>
  {
    public int LocacaoId { get; set; }
    public int FilmeId { get; set; }

    public virtual Locacao Locacao { get; set; }
    public virtual Filme Filme { get; set; }


    public override bool EhValido()
    {
      RuleFor(s => s.FilmeId)
        .NotEmpty().WithMessage("A locação deve haver filme");

      ValidationResult = Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
