using DesktopClient.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Shared.Dtos;
using Shared.Model;

namespace DesktopClient.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<ProductDto> _product;
        public ObservableCollection<ProductDto> Products
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
        public DelegateCommand<ProductDto> SubscribeProductCommand { get; private set; }
        public DelegateCommand<string> UnSubscribeProductCommand { get; private set; }

        public MainWindowViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            SearchProductCommand = new DelegateCommand(SearchProductAsync, CanSearchProduct);
            GoToWebSiteProductCommand = new DelegateCommand<string>(GoToWebSiteProduct, CanGoToWebSiteProduct);
            SubscribeProductCommand = new DelegateCommand<ProductDto>(SubscribeProduct, CanSubscribeProduct);
            UnSubscribeProductCommand = new DelegateCommand<string>(UnSubscribeProduct, CanUnSubscribeProduct);
            GetSubscribeProduct();
        }

        void SearchProductAsync()
        {
            if (!string.IsNullOrWhiteSpace(_text))
            {
                var products = new ObservableCollection<ProductDto>(Task.Run(() => _productRepository.GetProductsAsync(_text)).Result);
                Products = products;
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

        void SubscribeProduct(ProductDto productDto)
        {
            if (productDto != null)
            {
                Product product = new Product()
                {
                    Name = productDto.Name,
                    Link = productDto.Link,
                    Image = productDto.Image,
                    Rate = productDto.Rate,
                    Price = productDto.Price
                };

                var message = Task.Run(() => _productRepository.SubscribeProductAsync(product)).Result;
                if (message == "OK")
                {
                    SearchProductAsync();
                    GetSubscribeProduct();
                }
            }
        }

        bool CanSubscribeProduct(ProductDto product)
        {
            return true;
        }

        void UnSubscribeProduct(string link)
        {
            if (!string.IsNullOrWhiteSpace(link))
            {
                var message = (Task.Run(() => _productRepository.UnSubscribeProductsAsync(link)).Result);
                if (message == "OK")
                {
                    SearchProductAsync();
                    GetSubscribeProduct();
                }
            }
        }

        bool CanUnSubscribeProduct(string link)
        {
            return true;
        }

        void GetSubscribeProduct()
        {
            SubscribeProductCollection = new ObservableCollection<Product>(Task.Run(() => _productRepository.GetSubscribeProductsAsync()).Result);
        }
    }
}
