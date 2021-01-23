using api.ViewModels;
using AutoMapper;
using Reader.Domain;

namespace api.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Lote, LoteViewModel>();
            CreateMap<Produto, ProdutoViewModel>();
        }
    }
}