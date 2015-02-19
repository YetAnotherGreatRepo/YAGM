namespace Yagi.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using Yagi.Core.Model;
    using Yagi.Core.Parser;
    using Yagi.Core.Service;

    [TestFixture]
    public class QuoteServiceTests
    {
        [Test]
        public void GetNext_AfterTheLastQuote_ShouldReturnADifferentQuote()
        {
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
                fileName);
            var sut = new QuoteService(new QuoteXmlParser(filePath));

            Quote quote1 = sut.GetNext(new Quote());
            Quote quote2 = sut.GetNext(quote1);

            Assert.NotNull(quote1);
            Assert.That(quote2.Text, Is.Not.EqualTo(quote1.Text));
        }

        [Test]
        public void GetAll_ShouldReturnCollectionOfQuotes()
        {
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
                fileName);
            var sut = new QuoteService(new QuoteXmlParser(filePath));

            IEnumerable<Quote> quotes = sut.GetAll();

            Assert.IsTrue(quotes.Any());
        }
    }
}