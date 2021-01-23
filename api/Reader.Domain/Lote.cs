using System.Linq;
using System;
using System.Collections.Generic;
using Reader.Core.DomainObjects;

namespace Reader.Domain
{
    public class Lote : Entity
    {
        public DateTime DataImportacao { get; private set; }
        public int TotalProdutos { get; private set; }
        public double ValorTotal { get; private set; }
        public DateTime MenorDataEntrega { get; private set; }

        private readonly List<Produto> _produtos;
        public IReadOnlyCollection<Produto> Produtos => _produtos;
        public Lote()
        {}
        public Lote(List<Produto> produtos)
        {
            this._produtos = produtos;
            
            DataImportacao = DateTime.Now;
            CalcularQuantidadeTotalProdutos();
            CalcularMenorDataEntrega();
            CalcularValorTotalProdutos();
        }

        public void CalcularValorTotalProdutos()
        {
            this.ValorTotal = Math.Ceiling(Produtos.Sum(p => p.CalcularValorTotal()));
        }

        public void CalcularQuantidadeTotalProdutos()
        {
            this.TotalProdutos = Produtos.Count();
        }

        public void CalcularMenorDataEntrega()
        {
            this.MenorDataEntrega = Produtos.OrderBy(x => x.DataEntrega).FirstOrDefault().DataEntrega;
        }
    }
}