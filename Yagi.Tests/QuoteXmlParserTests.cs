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
            var invalidPath = String.Empty;
            var sut = new QuoteXmlParser(invalidPath);

            Assert.Throws<MissingQuotesFileException>(delegate() { sut.Load(); });
        }

        [Test]
        public void Load_GivenAValidPath_ShouldNotThrowException()
        {
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
                fileName);
            var sut = new QuoteXmlParser(filePath);

            Assert.DoesNotThrow(delegate() { sut.Load(); });
        }

        [Test]
        public void Load_GivenAValidPath_ShouldReturnAnyQuotes()
        {
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
                fileName);
            var sut = new QuoteXmlParser(filePath);

            IEnumerable<Quote> result = sut.Load();

            Assert.True(result.Any());
        }

        [Test]
        public void Load_GivenACollectionOfQuotes_ShouldReturnACompleteQuote()
        {
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
                fileName);
            var sut = new QuoteXmlParser(filePath);

            IEnumerable<Quote> result = sut.Load();

            Assert.That(result.First().Author, Is.EqualTo("benjaminRRR"));
        }
    }
}