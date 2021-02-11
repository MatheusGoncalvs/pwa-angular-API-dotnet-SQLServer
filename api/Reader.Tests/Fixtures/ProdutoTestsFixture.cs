using System;
using Reader.Domain;

namespace Reader.Tests.Fixtures
{
    public class ProdutoTestsFixture
    {
        public Produto GerarProdutoValido()
        {
            var dataEntrega = DateTime.Now;
            var produto = new Produto("Matheus Goncalves", 12, 5.40, dataEntrega);
            return produto;
        }
    }
}