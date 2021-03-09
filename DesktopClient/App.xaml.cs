using DesktopClient.Interfaces;
using DesktopClient.Services;
using DesktopClient.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped<IProductRepository, ProductRepository>();
        }
    }
}
