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
  [Route("genero")]
  public class GeneroController : BaseController
  {
    private readonly IGeneroRepository _generoRepository;
    private readonly IMapper _mapper;

    public GeneroController(
        IGeneroRepository generoRepository,
        IMapper mapper,
        IUser user) : base(user)
    {
      _generoRepository = generoRepository;
      _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    [Route("pegar-todos")]
    public IEnumerable<GeneroViewModel> PegarTodos()
    {
      var lst = _generoRepository.PegarTodos();
      return _mapper.Map<IEnumerable<GeneroViewModel>>(lst);
    }

    [Authorize]
    [Route("adicionar")]
    [HttpPost]
    public IActionResult Adicionar([FromBody] GeneroViewModel pGenero)
    {
      var genero = _mapper.Map<GeneroViewModel, Genero>(pGenero);

      if (genero.EhValido())
      {
        genero.Ativo = true;
        genero.DataCriacao = DateTime.Now;
        _generoRepository.Adicionar(genero);
        _generoRepository.SaveChanges();

        var retorno = new
        {
          id = genero.Id,
          nome = genero.Nome,
        };

        return Response(true, retorno);
      }
      else
        return Response(false, erros: genero.ValidationErrors);
    }

    [HttpPut]
    [Authorize]
    [Route("alterar")]
    public IActionResult Alterar([FromBody] GeneroViewModel pGenero)
    {
      var genero = _mapper.Map<GeneroViewModel, Genero>(pGenero);

      if (genero.EhValido())
      {
        _generoRepository.Atualizar(genero);
        _generoRepository.SaveChanges();

        return Response(true, pGenero);
      }
      return Response(false, erros: genero.ValidationErrors);
    }

    [Authorize]
    [Route("apagar")]
    [HttpDelete]
    public void Apagar([FromBody] List<int> pIds)
    {
      foreach (var id in pIds)
      {
        var genero = _generoRepository.PegarPorId(id);
        if (genero != null)
        {
          genero.Ativo = false;
          _generoRepository.Atualizar(genero);
        }
      }
      _generoRepository.SaveChanges();
    }
  }
}
