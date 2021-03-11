using DesktopClient.Interfaces;
using DesktopClient.Model;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using System.Threading;
using System.Timers;

namespace DesktopClient.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        //private string _snackBarMessage;
        //public string SnackBarMessage
        //{
        //    get { return _snackBarMessage; }
        //    set { SetProperty(ref _snackBarMessage, value); }
        //}

        private ObservableCollection<Product> _product;
        public ObservableCollection<Product> Products
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        private ObservableCollection<Product> _subscribeProductCollection;
        public ObservableCollection<Product> SubscribeProductCollection
        {
            get { return _subscribeProductCollection; }
            set { SetProperty(ref _subscribeProductCollection, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        readonly IProductRepository _productRepository;

        public DelegateCommand SearchProductCommand { get; private set; }
        public DelegateCommand<string> GoToWebSiteProductCommand { get; private set; }
        public DelegateCommand<Product> SubscribeProductCommand { get; private set; }
        public DelegateCommand<Product> UnSubscribeProductCommand { get; private set; }

        public MainWindowViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            SearchProductCommand = new DelegateCommand(SearchProductAsync, CanSearchProduct);
            GoToWebSiteProductCommand = new DelegateCommand<string>(GoToWebSiteProduct, CanGoToWebSiteProduct);
            SubscribeProductCommand = new DelegateCommand<Product>(SubscribeProduct, CanSubscribeProduct);
            UnSubscribeProductCommand = new DelegateCommand<Product>(UnSubscribeProduct, CanUnSubscribeProduct);
            GetSubscribeProduct();
        }

        void SearchProductAsync()
        {
            if (!string.IsNullOrWhiteSpace(_text))
            {
                var products = new ObservableCollection<Product>(Task.Run(() => _productRepository.GetProductsAsync(_text)).Result);
                Products = products;

                //SnackBarMessage = _text;
            }
        }

        bool CanSearchProduct()
        {
            return true;
        }

        void GoToWebSiteProduct(string id)
        {
            var url = $"https://www.ceneo.pl/{id}";

            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        bool CanGoToWebSiteProduct(string id)
        {
            return true;
        }

        void SubscribeProduct(Product product)
        {
            if (product != null)
            {
                var message = (Task.Run(() => _productRepository.SubscribeProductAsync(product)).Result);
                if (message == "OK")
                {
                    GetSubscribeProduct();
                }
            }
        }

        bool CanSubscribeProduct(Product product)
        {
            return true;
        }

        void UnSubscribeProduct(Product product)
        {
            if (product != null)
            {
                var message = (Task.Run(() => _productRepository.UnSubscribeProductsAsync(product)).Result);
                if (message == "OK")
                {
                    GetSubscribeProduct();
                }
            }
        }

        bool CanUnSubscribeProduct(Product product)
        {
            return true;
        }

        void GetSubscribeProduct()
        {
            SubscribeProductCollection = new ObservableCollection<Product>(Task.Run(() => _productRepository.GetSubscribeProductsAsync()).Result);
        }
    }
}
