using AutoMapper;
using Exs.Domain.Entities;
using Exs.Infra.Utils;

namespace Exs.Api.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMap<Genero, GeneroViewModel>();
      CreateMap<Filme, FilmeViewModel>();
      CreateMap<Locacao, LocacaoViewModel>();
      CreateMap<LocacaoFilme, LocacaoFilmeViewModel>();
    }
  }
}
