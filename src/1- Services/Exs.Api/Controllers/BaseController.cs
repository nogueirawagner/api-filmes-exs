using Exs.Domain.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Exs.Api.Controllers
{
  [Produces("application/json")]
  public abstract class BaseController : Controller
  {
    protected Guid UsuarioId { get; set; }
    
    protected BaseController(IUser user)
    {
      if (user.IsAuthenticated())
        UsuarioId = user.GetUserId();
    }

    protected new IActionResult Response(bool success, object result = null, object erros = null)
    {
      if (success)
      {
        return Ok(new
        {
          success = true,
          data = result
        });
      }

      return BadRequest(new
      {
        success = false,
        data = result,
        errors = erros
      });
    }
  }
}