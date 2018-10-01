using System;
using System.Collections.Generic;
using FluentValidation;

namespace Exs.Domain.Entities
{
  public class Genero : Entity<Genero>
  {
    public Genero()
    {
      DataCriacao = DateTime.Now;
      Ativo = true;
    }

    public string Nome { get; set; }
    public DateTime DataCriacao { get; set; }
    public bool Ativo { get; set; }
    public virtual IEnumerable<Filme> Filmes { get; set; }


    //Validações da Entidade
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
        .NotEmpty().WithMessage("O nome do gênero deve ser informado")
        .Length(0, 100);

      ValidationResult = Validate(this);

    }
  }
}
