using System.Collections.Generic;
using System.Threading.Tasks;
using api.ViewModels;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Reader.Domain;

namespace api.Services
{
    public interface IProdutoAppService
    {
        Task<List<Produto>> TranformarTabelaExcelParaListaProdutos(IFormFile file);
        Task<ValidationResult> AdicionarProdutos(IFormFile file);
        Task<List<LoteViewModel>> GetImportacoes();
        Task<List<ProdutoViewModel>> GetImportacao(int id);
        Lote RemoverImportacao(int id);
    }
}