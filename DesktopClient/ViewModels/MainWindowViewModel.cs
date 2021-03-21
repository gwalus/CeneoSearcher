using DesktopClient.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Shared.Dtos;
using Shared.Model;
using System.Linq;
using System.Windows;

namespace DesktopClient.ViewModels
{
    /// <summary>
    /// Main view.
    /// </summary>
    class MainWindowViewModel : BindableBase
    {
        /// <value>Loading product</value>
        private bool _loadButton;
        public bool LoadButton
        {
            get { return _loadButton; }
            set { SetProperty(ref _loadButton, value); }
        }

        /// <value>Collection of searched items</value>
        private ObservableCollection<ProductDto> _product;
        public ObservableCollection<ProductDto> Products
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        /// <value>Collection of subscribed items.</value>
        private ObservableCollection<Product> _subscribeProductCollection;
        public ObservableCollection<Product> SubscribeProductCollection
        {
            get { return _subscribeProductCollection; }
            set { SetProperty(ref _subscribeProductCollection, value); }
        }

        /// <value>Text from textbox</value>
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        readonly IProductRepository _productRepository;

        // Definition of DelegateCommand
        public DelegateCommand SearchProductCommand { get; private set; }
        public DelegateCommand<string> GoToWebSiteProductCommand { get; private set; }
        public DelegateCommand<ProductDto> SubscribeProductCommand { get; private set; }
        public DelegateCommand<string> UnSubscribeProductCommand { get; private set; }
        public DelegateCommand UpdateProductCommand { get; private set; }

        /// <summary>
        /// Constructor assigning a dependency injection and create a DelegateCommand instance.
        /// </summary>
        /// <param name="productRepository">DI product repository</param>
        public MainWindowViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            SearchProductCommand = new DelegateCommand(SearchProductAsync, CanSearchProduct);
            GoToWebSiteProductCommand = new DelegateCommand<string>(GoToWebSiteProduct, CanGoToWebSiteProduct);
            SubscribeProductCommand = new DelegateCommand<ProductDto>(SubscribeProduct, CanSubscribeProduct);
            UnSubscribeProductCommand = new DelegateCommand<string>(UnSubscribeProduct, CanUnSubscribeProduct);
            UpdateProductCommand = new DelegateCommand(UpdateProduct, CanUpdateProduct);
            LoadButton = false;
            GetSubscribeProduct();
        }

        /// <summary>
        /// Method executed when calling the SearchProductCommand command, which gets the collection of products by name stored in _text and writes to the Products variable.
        /// </summary>
        async void SearchProductAsync()
        {
            if (!string.IsNullOrWhiteSpace(_text))
            {
                var products = new ObservableCollection<ProductDto>(await Task.Run(() => _productRepository.GetProductsAsync(_text)));
                Products = products;
            }
        }

        /// <summary>
        /// A method that checks if the SearchProductAsync method can be executed.
        /// </summary>
        /// <returns>bool</returns>
        bool CanSearchProduct()
        {
            return true;
        }

        /// <summary>
        /// Method executed when calling the GoToWebSiteProductCommand command to open the product page.
        /// </summary>
        /// <param name="id">id as string</param>
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

        /// <summary>
        /// A method that checks if the GoToWebSiteProduct method can be executed
        /// </summary>
        /// <param name="id">Parameter as string</param>
        /// <returns>bool</returns>
        bool CanGoToWebSiteProduct(string id)
        {
            return true;
        }

        /// <summary>
        ///  Method executed when calling the SubscribeProductCommand command to query the product to subscribe to.
        /// </summary>
        /// <param name="productDto">productDto as ProductDto</param>
        async void SubscribeProduct(ProductDto productDto)
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

                var message = await Task.Run(() => _productRepository.SubscribeProductAsync(product));
                if (message == "OK")
                {
                    Products.FirstOrDefault(p => p == productDto).IsSubscribed = true;
                    Products = new ObservableCollection<ProductDto>(Products);
                    SubscribeProductCollection.Add(product);
                }
            }
        }

        /// <summary>
        /// A method that checks if the SubscribeProduct method can be executed..
        /// </summary>
        /// <param name="product">Parametr as ProductDto</param>
        /// <returns>bool</returns>
        bool CanSubscribeProduct(ProductDto product)
        {
            return true;
        }

        /// <summary>
        /// Method executed when calling the UnSubscribeProductCommand command, querying the product id to be unsubscribed.
        /// </summary>
        /// <param name="link"></param>
        async void UnSubscribeProduct(string link)
        {
            if (!string.IsNullOrWhiteSpace(link))
            {
                var message = await Task.Run(() => _productRepository.UnSubscribeProductsAsync(link));
                if (message == "OK")
                {
                    if (Products != null)
                    {
                        var p = Products.FirstOrDefault(p => p.Link == link);
                        if (p != null)
                        {
                            p.IsSubscribed = false;
                            Products = new ObservableCollection<ProductDto>(Products);
                        }
                    }
                    SubscribeProductCollection.Remove(SubscribeProductCollection.FirstOrDefault(p => p.Link == link));
                }
            }
        }

        /// <summary>
        /// A method that checks if the UnSubscribeProduct method can be executed.
        /// </summary>
        /// <param name="link">Parametr as string</param>
        /// <returns>bool</returns>
        bool CanUnSubscribeProduct(string link)
        {
            return true;
        }

        /// <summary>
        /// A method that gets and enters the collection of the subscribed products into the SubscribeProductCollection field.
        /// </summary>
        async void GetSubscribeProduct()
        {
            SubscribeProductCollection = new ObservableCollection<Product>(await Task.Run(() => _productRepository.GetSubscribeProductsAsync()));
        }

        /// <summary>
        /// Database update method.
        /// </summary>
        async void UpdateProduct()
        {
            LoadButton = true;
            var products = await Task.Run(() => _productRepository.UpdateProductsAsync());
            if (products != null) SubscribeProductCollection = new ObservableCollection<Product>(products);
            LoadButton = false;
        }

        /// <summary>
        /// A method that checks if the UpdateProduct method can be executed.
        /// </summary>
        /// <returns>Wartość bool</returns>
        bool CanUpdateProduct() 
        {
            return true;
        }
    }
}
