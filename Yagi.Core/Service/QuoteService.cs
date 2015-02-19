namespace Yagi.Core.Service
{
    using System;

    using Yagi.Core.Model;

    public class QuoteService
    {
        private readonly Random random = new Random();

        public Quote GetNext()
        {
            var quote = new Quote { Text = "" + random.Next(1, 100) };

            return quote;
        }
    }
}