using System;
using Reader.Domain;

namespace Reader.Tests.Fixtures
{
    public class ProdutoTestsFixture
    {
        public Produto GerarProdutoValido()
        {
            var dataEntrega = DateTime.Now;
            var produto = new Produto("Matheus Goncalves", 10, 5.0, dataEntrega);
            return produto;
        }
    }
}