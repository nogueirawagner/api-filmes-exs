using AutoMapper;
using Exs.Domain.Entities;
using Exs.Infra.Utils;

namespace Exs.Api.AutoMapper
{
  public class ViewModelToDomainMappingProfile : Profile
  {
    public ViewModelToDomainMappingProfile()
    {
      CreateMap<GeneroViewModel, Genero>();
      CreateMap<FilmeViewModel, Filme>();
      CreateMap<LocacaoViewModel, Locacao>();
      CreateMap<LocacaoFilmeViewModel, LocacaoFilme>();
    }
  }
}
