namespace Yagi.UI
{
    using System;
    using System.Configuration;
    using System.Timers;
    using System.Windows;

    using Yagi.Core.Model;
    using Yagi.Core.Parser;
    using Yagi.Core.Service;
    using Yagi.UI.ViewModel;
    using Yagi.UI.Windows;

    public partial class MainWindow : Window
    {
        private Quote quote;

        private readonly IQuoteService quoteService;

        private QuoteWindow quoteWindow;

        private readonly Timer timer;

        public MainWindow()
        {
            var filePath = ConfigurationManager.AppSettings["QuoteData"];
            quoteService = new QuoteService(new QuoteXmlParser(filePath));

            timer = new Timer(20000);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;

            InitializeComponent();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (quote == null)
            {
                quote = new Quote();
            }
            quote = quoteService.GetNext(quote);

            if (quote != null)
            {
                var d = Application.Current.Dispatcher;
                if (d.CheckAccess())
                {
                    OnChangedInMainThread();
                }
                else
                {
                    d.BeginInvoke((Action)OnChangedInMainThread);
                }
            }
        }

        private void OnChangedInMainThread()
        {
            var viewModel = new QuoteWindowViewModel() { Author = quote.Author, Text = quote.Text };
            quoteWindow = new QuoteWindow();

            quoteWindow.DataContext = viewModel;
            quoteWindow.Show();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (quoteWindow != null)
            {
                var viewModel = (QuoteWindowViewModel)quoteWindow.DataContext;

                quote = quoteService.GetNext(quote ?? new Quote());

                quoteWindow.Timer.Stop();
                timer.Stop();
                quoteWindow.Timer.Start();
                timer.Start();

                viewModel.Text = quote.Text;
                viewModel.Author = quote.Author;
            }
           }
    }
}