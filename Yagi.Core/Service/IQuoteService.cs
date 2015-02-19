namespace Yagi.Core.Service
{
    using System.Collections.Generic;

    using Yagi.Core.Model;

    public interface IQuoteService
    {
        Quote GetNext(Quote lastQuote);

        IEnumerable<Quote> GetAll();
    }
}