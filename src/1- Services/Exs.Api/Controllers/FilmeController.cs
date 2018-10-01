using AutoMapper;
using Exs.Domain.Entities;
using Exs.Domain.Interfaces;
using Exs.Domain.IRepositories;
using Exs.Infra.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Exs.Api.Controllers
{
  [Route("filme")]
  public class FilmeController : BaseController
  {
    private readonly IFilmeRepository _filmeRepository;
    private readonly IMapper _mapper;

    public FilmeController(
        IFilmeRepository filmeRepository,
        IMapper mapper,
        IUser user) : base(user)
    {
      _filmeRepository = filmeRepository;
      _mapper = mapper;
    }

    [Authorize]
    [Route("adicionar")]
    [HttpPost]
    public IActionResult Adicionar([FromBody] FilmeViewModel pFilme)
    {
      var filme = _mapper.Map<FilmeViewModel, Filme>(pFilme);   // cria mapeamento da view model com entidade via automapper.
      _filmeRepository.Adicionar(filme);  // Add do entity framework

      if (filme.EhValido())
      {
        _filmeRepository.SaveChanges();   // salva as alterações

        // monta um obj anonimo para retono.
        var retorno = new
        {
          id = filme.Id,
          nome = filme.Nome,
          genero = filme.GeneroId
        };

        return Response(true, retorno); // Retorna Ok código 200 para requisição
      }

      // Retorna 400.
      return Response(false, erros: filme.ValidationErrors);
    }

    [HttpGet]
    [Authorize]
    [Route("pegar-todos")]
    public IEnumerable<FilmeViewModel> PegarTodos()
    {
      var lst = _filmeRepository.PegarTodos(); // pegando todos utilizando dapper.
      return _mapper.Map<IEnumerable<FilmeViewModel>>(lst);
    }

    [HttpPut]
    [Authorize]
    [Route("alterar")]
    public IActionResult Alterar([FromBody] FilmeViewModel pFilme)
    {
      var filme = _mapper.Map<FilmeViewModel, Filme>(pFilme);

      if (filme.EhValido())
      {
        _filmeRepository.Atualizar(filme);
        _filmeRepository.SaveChanges();

        return Response(true, filme);
      }
      return Response(false, erros: filme.ValidationErrors);
    }

    [Authorize]
    [Route("apagar")]
    [HttpDelete]
    public void Apagar([FromBody] List<int> pIds)
    {
      foreach (var id in pIds)
      {
        var filme = _filmeRepository.PegarPorId(id);
        if (filme != null)
        {
          filme.Ativo = false;
          _filmeRepository.Atualizar(filme);
        }

      }
      _filmeRepository.SaveChanges();
    }
  }
}