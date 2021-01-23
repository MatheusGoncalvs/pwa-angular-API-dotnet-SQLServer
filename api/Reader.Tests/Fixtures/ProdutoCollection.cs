using Xunit;

namespace Reader.Tests.Fixtures
{
    [CollectionDefinition(nameof(ProdutoCollection))]
    public class ProdutoCollection : ICollectionFixture<ProdutoTestsFixture>
    {

    }
}