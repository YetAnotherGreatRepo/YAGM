using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Yagi.UI
{
    using System.IO;

    using Yagi.Core.Parser;
    using Yagi.Core.Service;
    using Yagi.UI.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var window = new QuoteWindow();
            window.Show();
        }
    }
}
