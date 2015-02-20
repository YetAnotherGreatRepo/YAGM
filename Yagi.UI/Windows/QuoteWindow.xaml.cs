namespace Yagi.UI.Windows
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Timers;
    using System.Windows;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    using Yagi.Core.Model;

    public partial class QuoteWindow : Window
    {
        public static Quote CurrentQuote { get; set; }

        public DispatcherTimer Timer;

        public QuoteWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
            anim.Completed += (s, _) => this.Close();
            BeginAnimation(OpacityProperty, anim);
        }

        private void QuoteWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(12d);
            Timer.Tick += TimerTick;
            Timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Timer = (DispatcherTimer)sender;
            Timer.Stop();
            Timer.Tick -= TimerTick;
            Close();
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double windowWidth = this.Width;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = 10;
        }
    }
}
