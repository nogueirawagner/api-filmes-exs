using Exs.Domain.Interfaces;
using Exs.Domain.IRepositories;
using Exs.Infra.Data.Context;
using Exs.Infra.Data.Repository;
using Exs.Infra.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Exs.Api.Configurations
{
  public static class DependencyInjectionConfiguration
  {
    public static void AddDIConfiguration(this IServiceCollection services)
    {
      // ASPNET
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      // Infra - Identity
      services.AddScoped<IUser, AspNetUser>();

      // Infra - Data
      services.AddScoped<IFilmeRepository, FilmeRepository>();
      services.AddScoped<IGeneroRepository, GeneroRepository>();
      services.AddScoped<ILocacaoRepository, LocacaoRepository>();
      services.AddScoped<ILocacaoFilmeRepository, LocacaoFilmeRepository>();
      services.AddScoped<ContextDb>();
    }
  }
}