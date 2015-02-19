namespace Yagi.Core.Service
{
    using Yagi.Core.Model;

    public interface IQuoteService
    {
        Quote GetNext();
    }
}