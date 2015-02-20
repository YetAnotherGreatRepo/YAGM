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

        private QuoteWindow quoteWindow;

        private readonly IQuoteService quoteService;

        private readonly Timer timer;

        public MainWindow()
        {
            this.quoteService = App.GetInstance<IQuoteService>();

            ProcessQuote();
            timer = new Timer(TimeSpan.FromHours(1).TotalMilliseconds);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;

            InitializeComponent();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            ProcessQuote();
        }

        private void ProcessQuote()
        {
            quote = quoteService.GetNext(quote ?? new Quote());

            if (quote != null)
            {
                var d = Application.Current.Dispatcher;

                if (d.CheckAccess())
                {
                    DisplayNextQuote();
                }
                else
                {
                    d.BeginInvoke((Action)DisplayNextQuote);
                }
            }
        }

        private void DisplayNextQuote()
        {
            if (quoteWindow != null)
            {

                quoteWindow.Close();
            }

            quote = quoteService.GetNext(quote ?? new Quote());

            quoteWindow = new QuoteWindow();
            
            var viewModel = (QuoteWindowViewModel)quoteWindow.DataContext;
            viewModel.Text = quote.Text;
            viewModel.Author = quote.Author;

            quoteWindow.Show();
        }

        private void NextQuote_OnClick(object sender, RoutedEventArgs e)
        {
            DisplayNextQuote();
        }


        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            if (quoteWindow != null)
            {
                quoteWindow.Close();
            }

            this.Close();
        }
    }
}