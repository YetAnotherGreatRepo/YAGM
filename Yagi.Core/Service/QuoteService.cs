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
        private static List<Quote> availableQuotes;

        private static List<Quote> usedQuotes;

        private readonly IQuoteXmlParser quoteXmlParser;

        private readonly Random random;

        public QuoteService(IQuoteXmlParser quoteXmlParser)
        {
            this.quoteXmlParser = quoteXmlParser;
            usedQuotes = new List<Quote>();
            random = new Random();
        }

        public Quote GetNext(Quote lastQuote)
        {
            if (availableQuotes == null)
            {
                availableQuotes = GetAll().ToList();
            }

            while (true)
            {
                // Check if we're using the last quote we haven't used before
                // After this quote, we are starting over with a new empty usedQuotes list
                var quotes = availableQuotes.Except(usedQuotes).ToList();

                if (quotes.Count() == 1)
                {
                    var output = quotes.FirstOrDefault();
                    usedQuotes.Clear();
                    usedQuotes.Add(output);
                    return output;
                }

                // We have more than 1 remaining quote, so proceed with normal behaviour
                var rndIndex = random.Next(0, quotes.Count());
                var newQuote = quotes[rndIndex];

                if (newQuote != null && 
                    newQuote.Text != lastQuote.Text &&
                    !usedQuotes.Contains(newQuote))
                {
                    usedQuotes.Add(newQuote);
                    return newQuote;
                }
            }

            throw new InstanceNotFoundException();
        }

        public IEnumerable<Quote> GetAll()
        {
            try
            {
                if (availableQuotes != null)
                {
                    return availableQuotes;
                }
                availableQuotes = quoteXmlParser.Load().ToList();
                return availableQuotes;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not load quotes from quoteXmlParser");
            }
        }
    }
}