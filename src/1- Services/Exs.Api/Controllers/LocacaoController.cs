using AutoMapper;
using Exs.Domain.Entities;
using Exs.Domain.Interfaces;
using Exs.Domain.IRepositories;
using Exs.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exs.Api.Controllers
{
  [Route("locacao")]
  public class LocacaoController : BaseController
  {
    private readonly ILocacaoRepository _locacaoRepository;
    private readonly IFilmeRepository _filmeRepository;
    private readonly ILocacaoFilmeRepository _locacaoFilmeRepository;
    private readonly IMapper _mapper;

    public LocacaoController(
        IFilmeRepository filmeRepository,
        ILocacaoFilmeRepository locacaoFilmeRepository,
        ILocacaoRepository locacaoRepository,
        IMapper mapper,
        IUser user) : base(user)
    {
      _filmeRepository = filmeRepository;
      _locacaoFilmeRepository = locacaoFilmeRepository;
      _locacaoRepository = locacaoRepository;
      _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    [Route("pegar-todas")]
    public IEnumerable<LocacaoViewModel> PegarTodas()
    {
      var locacoes = _locacaoRepository.PegarTodos(UsuarioCPF);
      var locacoesFilme = _locacaoFilmeRepository.PegarLocacoesFilme(UsuarioCPF);

      MontarFilmesDaLocacao(locacoes.ToList(), locacoesFilme);
      return _mapper.Map<IEnumerable<LocacaoViewModel>>(locacoes);
    }

    [Authorize]
    [Route("adicionar")]
    [HttpPost]
    public IActionResult Adicionar([FromBody] LocacaoViewModel pLocacao)
    {
      pLocacao.DataLocacao = DateTime.Now;
      pLocacao.Locacoes = null;
      var locacao = _mapper.Map<LocacaoViewModel, Locacao>(pLocacao);
      try
      {
        _locacaoRepository.Adicionar(locacao);
        _locacaoRepository.SaveChanges();

        foreach (var fId in pLocacao.FilmesId)
        {
          var locFilme = new LocacaoFilme
          {
            FilmeId = fId,
            LocacaoId = locacao.Id
          };
          _locacaoFilmeRepository.Adicionar(locFilme);
          _locacaoFilmeRepository.SaveChanges();
        }

        var locacoesFilme = _locacaoFilmeRepository.PegarLocacoesFilmePorLocacaoId(locacao.Id, UsuarioCPF);
        MontarFilmesDaLocacao(locacao, locacoesFilme);

        var retorno = _mapper.Map<Locacao, LocacaoViewModel>(locacao);
        return Response(true, retorno);
      }
      catch (Exception)
      {
        return Response(false, erros: locacao.ValidationErrors);
      }
    }

    [HttpPut]
    [Authorize]
    [Route("alterar")]
    public IActionResult Alterar([FromBody] LocacaoViewModel pLocacao)
    {
      var locacao = _mapper.Map<LocacaoViewModel, Locacao>(pLocacao);

      var locacoesFilme = _locacaoFilmeRepository.Pegar(s => s.LocacaoId == locacao.Id).ToList();
      foreach (var lcFilme in locacoesFilme)
      {
        _locacaoFilmeRepository.Remover(lcFilme.Id);
        _locacaoFilmeRepository.SaveChanges();
      }

      foreach (var fId in pLocacao.FilmesId)
      {
        var locFilme = new LocacaoFilme
        {
          FilmeId = fId,
          LocacaoId = locacao.Id
        };
        if (locFilme.EhValido())
        {
          _locacaoFilmeRepository.Adicionar(locFilme);
          _locacaoFilmeRepository.SaveChanges();
        }
      }

      var locFilmes = _locacaoFilmeRepository.PegarLocacoesFilmePorLocacaoId(locacao.Id, UsuarioCPF);
      MontarFilmesDaLocacao(locacao, locFilmes);

      var retorno = _mapper.Map<Locacao, LocacaoViewModel>(locacao);
      return Response(true, retorno);
    }

    [Authorize]
    [Route("apagar")]
    [HttpDelete]
    public void Apagar([FromBody] List<int> pIds)
    {
      _locacaoRepository.Remover(pIds);
      _locacaoRepository.SaveChanges();
    }

    private static void MontarFilmesDaLocacao(List<Locacao> locacoes, IEnumerable<IGrouping<int, LocacaoFilme>> locacoesFilme)
    {
      foreach (var locacao in locacoes)
      {
        locacao.Locacoes = new List<LocacaoFilme>();
        var locFilmes = locacoesFilme.Where(s => s.Key == locacao.Id).FirstOrDefault();
        if (locFilmes != null)
          locacao.Locacoes = locFilmes.Select(x => x).ToArray();
      }
    }

    private static void MontarFilmesDaLocacao(Locacao locacao, IEnumerable<IGrouping<int, LocacaoFilme>> locacoesFilme)
    {
      locacao.Locacoes = new List<LocacaoFilme>();
      var locFilmes = locacoesFilme.Where(s => s.Key == locacao.Id).FirstOrDefault();
      if (locFilmes != null)
        locacao.Locacoes = locFilmes.Select(x => x).ToArray();
    }
  }
}