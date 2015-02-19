namespace Yagi.Tests
{
    using NUnit.Framework;

    using Yagi.Core.Model;
    using Yagi.Core.Service;

    [TestFixture]
    public class QuoteServiceTests
    {
        [Test]
        public void GetNext_AfterTheLastQuote_ShouldReturnADifferentQuote()
        {
            var sut = new QuoteService();

            Quote quote1 = sut.GetNext();
            Quote quote2 = sut.GetNext();

            Assert.NotNull(quote1);
            Assert.That(quote2.Text, Is.Not.EqualTo(quote1.Text));
        }
    }
}