namespace Yagi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using Yagi.Core.Model;

    [TestFixture]
    public class QuoteXmlParserTests
    {
        [Test]
        public void Load_GivenAnInvalidPath_ShouldThrowMissingQuotesException()
        {
            var sut = new QuoteXMLParser();
            var invalidPath = String.Empty;

            Assert.Throws<MissingQuotesFileException>(delegate() { sut.Load(invalidPath); });
        }

        [Test]
        public void Load_GivenAValidPath_ShouldNotThrowException()
        {
            var sut = new QuoteXMLParser();
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())),
                fileName);

            Assert.DoesNotThrow(delegate() { sut.Load(filePath); });
        }

        [Test]
        public void Load_GivenAValidPath_ShouldReturnAnyQuotes()
        {
            var sut = new QuoteXMLParser();
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())),
                fileName);

            IEnumerable<Quote> result = sut.Load(filePath);

            Assert.True(result.Any());
        }
    }

    public class QuoteXMLParser
    {
        public IEnumerable<Quote> Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                return new List<Quote>(){ new Quote()};
            }
            throw new MissingQuotesFileException();
        }
    }

    public class MissingQuotesFileException : Exception
    {
    }
}