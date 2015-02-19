namespace Yagi.Core.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Instrumentation;

    using Yagi.Core.Model;
    using Yagi.Core.Parser;

    public class QuoteService : IQuoteService
    {
        private IQuoteXmlParser quoteXmlParser;

        private Random random;

        public QuoteService(IQuoteXmlParser quoteXmlParser)
        {
            this.quoteXmlParser = quoteXmlParser;
            this.random = new Random();
        }

        public Quote GetNext(Quote lastQuote)
        {
            var quotes = GetAll().ToList();

            if (quotes.Any())
            {
                while (true)
                {
                    var rndIndex = random.Next(0, quotes.Count() - 1);
                    var newQuote = quotes[rndIndex];

                    if (newQuote != null && newQuote.Text != lastQuote.Text)
                    {
                        return newQuote;
                    }
                }
            }

            throw new InstanceNotFoundException();
        }

        public IEnumerable<Quote> GetAll()
        {
            try
            {
                var quotes = quoteXmlParser.Load();
                return quotes;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not load quotes from quoteXmlParser");
            }
        }
    }
}