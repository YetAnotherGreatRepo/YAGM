namespace Yagi.UI.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Yagi.Core.Model;
    using Yagi.UI.Annotations;

    public class QuoteWindowViewModel : INotifyPropertyChanged
    {
        private string text;

        private string author;

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