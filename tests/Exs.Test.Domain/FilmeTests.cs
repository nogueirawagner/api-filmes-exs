using Exs.Domain.Entities;
using Exs.Domain.IRepositories;
using System;
using System.Linq;
using Xunit;

namespace Exs.Test.Domain
{
  public class FilmeTests
  {
    [Fact(DisplayName = "Instanciar Filme válido.")]
    [Trait("Domain", "Filmes")]
    public void Filmes_Instanciar_Valido()
    {
      var filme = new Filme()
      {
        Nome = "Homem-Aranha: De Volta Ao Lar",
        DataCriacao = DateTime.Now,
        Ativo = true,
        GeneroId = 1
      };
      var isValid = filme.EhValido();
      Assert.True(isValid);
    }

    [Fact(DisplayName = "Validar Nome vazio.")]
    [Trait("Domain", "Filmes")]
    public void Filme_Nome_Vazio()
    {
      var filme = new Filme()
      {
        Nome = "",
        DataCriacao = DateTime.Now,
        Ativo = true,
        GeneroId = 1
      };

      Assert.False(filme.EhValido());
      Assert.Equal("O nome do filme deve ser informado", filme.ValidationErrors.First());
    }

    [Fact(DisplayName = "Validar Nome Maior Permitido.")]
    [Trait("Domain", "Filmes")]
    public void Filme_Nome_Grande()
    {
      var nomeGrande = @"Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser integrado a softwares de editoração eletrônica como Aldus PageMaker. Ao contrário do que se acredita, Lorem Ipsum não é simplesmente um texto randômico. Com mais de 2000 anos, suas raízes podem ser encontradas em uma obra de literatura latina clássica datada de 45 AC. Richard McClintock, um professor de latim do Hampden-Sydney College na Virginia, pesquisou uma das mais obscuras palavras em latim, consectetur, oriunda de uma passagem de Lorem Ipsum, e, procurando por entre citações da palavra na literatura clássica, descobriu a sua indubitável origem. ";

      var filme = new Filme()
      {
        Nome = nomeGrande,
        GeneroId = 1
      };

      Assert.False(filme.EhValido());
      Assert.Equal("O nome precisa de conter no máximo 200 caracteres", filme.ValidationErrors.First());
    }

    [Fact(DisplayName = "Validar Filme sem Gênero")]
    [Trait("Domain", "Filmes")]
    public void Filme_Sem_Genero()
    {
      var filme = new Filme()
      {
        Nome = "Letraset",
      };

      Assert.False(filme.EhValido());
      Assert.Equal("O gênero do filme deve ser informado", filme.ValidationErrors.First());
    }

    [Fact(DisplayName = "Validar Nome Maior Permitido Sem Genero.")]
    [Trait("Domain", "Filmes")]
    public void Filme_Nome_Grande_Sem_Genero()
    {
      var nomeGrande = @"Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser integrado a softwares de editoração eletrônica como Aldus PageMaker. Ao contrário do que se acredita, Lorem Ipsum não é simplesmente um texto randômico. Com mais de 2000 anos, suas raízes podem ser encontradas em uma obra de literatura latina clássica datada de 45 AC. Richard McClintock, um professor de latim do Hampden-Sydney College na Virginia, pesquisou uma das mais obscuras palavras em latim, consectetur, oriunda de uma passagem de Lorem Ipsum, e, procurando por entre citações da palavra na literatura clássica, descobriu a sua indubitável origem. ";

      var filme = new Filme()
      {
        Nome = nomeGrande
      };

      Assert.False(filme.EhValido());
      Assert.Equal(2, filme.ValidationErrors.Count);
      Assert.True(filme.ValidationErrors.Exists(s => s == "O nome precisa de conter no máximo 200 caracteres"));
      Assert.True(filme.ValidationErrors.Exists(s => s == "O gênero do filme deve ser informado"));
    }

    [Fact(DisplayName = "Validar filme sem Nome e Sem Genero.")]
    [Trait("Domain", "Filmes")]
    public void Filme_Sem_Nome_Sem_Genero()
    {
      var filme = new Filme()
      {
      };

      Assert.False(filme.EhValido());
      Assert.Equal(2, filme.ValidationErrors.Count);
      Assert.True(filme.ValidationErrors.Exists(s => s == "O nome do filme deve ser informado"));
      Assert.True(filme.ValidationErrors.Exists(s => s == "O gênero do filme deve ser informado"));
    }
  }
}