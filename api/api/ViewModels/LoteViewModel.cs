using System;

namespace api.ViewModels
{
    public class LoteViewModel
    {
        public int Id { get; set; }
        public DateTime DataImportacao { get; set; }
        public int TotalProdutos { get; set; }
        public double ValorTotal { get; set; }
        public DateTime MenorDataEntrega { get; set; }
    }
}