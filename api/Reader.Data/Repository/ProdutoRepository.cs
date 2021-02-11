using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reader.Domain;

namespace Reader.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoContext _context;

        public ProdutoRepository(ProdutoContext context)
        {
            this._context = context;
        }
        public async Task AdicionarLote(Lote lote)
        {
            await _context.Lotes.AddAsync(lote);
        }
        public async Task<List<Produto>> GetImportacao(int id)
        {
            return await _context.Produtos.Where(x => x.IdLote == id).ToListAsync();
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Lote>> GetImportacoes()
        {
            return await _context.Lotes.ToListAsync();
        }

        public Lote RemoverImportacao(int id)
        {
            var lote = _context.Lotes.Where(x => x.Id == id).FirstOrDefault();
            _context.Remove(lote);

            return lote;
        }
    }
}