using api.ViewModels;
using AutoMapper;
using Reader.Domain;

namespace api.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProdutoViewModel, Produto>()
                .ConstructUsing(p =>
                    new Produto(p.Nome, p.Quantidade, p.ValorUnitario, p.DataEntrega));
        }
    }
}