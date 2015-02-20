namespace Yagi.UI
{
    using System.Configuration;
    using System.Windows;

    using SimpleInjector;

    using Yagi.Core.Parser;
    using Yagi.Core.Service;

    public partial class App : Application
    {
        private static Container container;

        [System.Diagnostics.DebuggerStepThrough]
        public static TService GetInstance<TService>() where TService : class
        {
            return container.GetInstance<TService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            BootstrapDependencyInjector();
        }

        private void BootstrapDependencyInjector()
        {
            var injector = new Container();

            injector.Register<IQuoteXmlParser>(() => new QuoteXmlParser(ConfigurationManager.AppSettings["QuoteData"]));

            injector.Register<IQuoteService, QuoteService>();

            injector.Verify();

            container = injector;
        }
    }
}
