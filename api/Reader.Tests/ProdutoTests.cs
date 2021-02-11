using Reader.Tests.Fixtures;
using Xunit;

namespace Reader.Tests
{
    [Collection(nameof(ProdutoCollection))]
    public class ProdutoTests
    {
        private readonly ProdutoTestsFixture _produtoTestsFixture;

        public ProdutoTests(ProdutoTestsFixture produtoTestsFixture)
        {
            this._produtoTestsFixture = produtoTestsFixture;
        }

        [Fact(DisplayName = "Novo Produto Válido")]
        [Trait("Categoria", "Produto Fixture Testes")]
        public void Produto_ValorTotal_CalcularValorTotalCorretamente()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();
            var valorTotal = 64.8;

            //Act
            produto.CalcularValorTotal();

            //Assert
            Assert.Equal(valorTotal, produto.ValorTotal);
        }
    }
}
