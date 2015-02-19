namespace Yagi.UI.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    using Yagi.Core.Model;
    using Yagi.Core.Parser;
    using Yagi.Core.Service;
    using Yagi.UI.Annotations;

    public class QuoteWindowViewModel : INotifyPropertyChanged
    {
        private string text;

        private string author;

        public QuoteWindowViewModel()
        {
            var fileName = "quotes.xml";
            var filePath = String.Format(
                @"{0}\{1}",
                Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
                fileName);
            var quoteService = new QuoteService(new QuoteXmlParser(filePath));

            Quote quote = new Quote();

            if (!String.IsNullOrWhiteSpace(text))
            {
                quote = quoteService.GetNext(new Quote() { Text = text });
            }
            else
            {
                quote = quoteService.GetNext(new Quote());
            }

            this.Text = quote.Text;
            this.Author = quote.Author;
        }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}