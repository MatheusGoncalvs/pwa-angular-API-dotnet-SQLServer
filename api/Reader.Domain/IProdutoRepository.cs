using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Reader.Domain
{
    public interface IProdutoRepository
    {
        Task AdicionarLote(Lote lote);
        Task<List<Lote>> GetImportacoes();
        Task<List<Produto>> GetImportacao(int id);
        Task Commit();
    }
}