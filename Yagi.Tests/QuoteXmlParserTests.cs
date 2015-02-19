namespace Yagi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using Yagi.Core.Exception;
    using Yagi.Core.Model;
    using Yagi.Core.Parser;

    [TestFixture]
    public class QuoteXmlParserTests
    {
        [Test]
        public void Load_GivenAnInvalidPath_ShouldThrowMissingQuotesException()
        {
            var sut = new QuoteXmlParser();
            var invalidPath = String.Empty;

            Assert.Throws<MissingQuotesFileException>(delegate() { sut.Load(invalidPath); });
        }

        [Test]
        public void Load_GivenAValidPath_ShouldNotThrowException()
        {
            var sut = new QuoteXmlParser();
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
                fileName);

            Assert.DoesNotThrow(delegate() { sut.Load(filePath); });
        }

        [Test]
        public void Load_GivenAValidPath_ShouldReturnAnyQuotes()
        {
            var sut = new QuoteXmlParser();
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
                fileName);

            IEnumerable<Quote> result = sut.Load(filePath);

            Assert.True(result.Any());
        }

        [Test]
        public void Load_GivenACollectionOfQuotes_ShouldReturnACompleteQuote()
        {
            var sut = new QuoteXmlParser();
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
                fileName);

            IEnumerable<Quote> result = sut.Load(filePath);

            Assert.That(result.First().Author, Is.EqualTo("benjaminRRR"));
        }
    }
}