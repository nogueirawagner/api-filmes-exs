using FluentValidation;
using System;
using System.Collections.Generic;

namespace Exs.Domain.Entities
{
  public class Filme : Entity<Filme>
  {
    public Filme()
    {
      Ativo = true;
      DataCriacao = DateTime.Now;
    }

    public string Nome { get; set; }
    public DateTime DataCriacao { get; set; }
    public bool Ativo { get; set; }
    public int GeneroId { get; set; }

    // Propriedades de navegação para o EF.
    public virtual Genero Genero { get; set; }
    public virtual IEnumerable<LocacaoFilme> Locacoes { get; set; }

    public override bool EhValido()
    {
      Validar();
      foreach (var erro in ValidationResult.Errors)
        ValidationErrors.Add(erro.ErrorMessage);

      return ValidationResult.IsValid;
    }

    private void Validar()
    {
      RuleFor(s => s.Nome)
       .NotEmpty().WithMessage("O nome do filme deve ser informado")
       .Length(0, 200).WithMessage("O nome precisa de conter no máximo 200 caracteres");

      RuleFor(s => s.GeneroId)
        .GreaterThan(0).WithMessage("O gênero do filme deve ser informado");

      ValidationResult = Validate(this);
    }
  }
}

