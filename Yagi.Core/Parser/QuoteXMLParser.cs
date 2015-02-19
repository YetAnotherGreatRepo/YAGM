namespace Yagi.Core.Parser
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using Yagi.Core.Exception;
    using Yagi.Core.Model;

    public class QuoteXmlParser : IQuoteXmlParser
    {
        public IEnumerable<Quote> Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(QuoteCollection));
                StreamReader reader = new StreamReader(filePath);
                var quotes = (QuoteCollection)serializer.Deserialize(reader);
                reader.Close();

                return quotes.Quotes;
            }
            throw new MissingQuotesFileException();
        }
    }
}