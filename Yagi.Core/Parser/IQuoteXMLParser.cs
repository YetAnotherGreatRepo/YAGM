namespace Yagi.Core.Parser
{
    using System.Collections.Generic;

    using Yagi.Core.Model;

    public interface IQuoteXmlParser
    {
        IEnumerable<Quote> Load(string filePath);
    }
}