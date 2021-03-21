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

namespace DesktopClient.ViewModels
{
    /// <summary>
    /// Klasa główna modelu widoku.
    /// </summary>
    class MainWindowViewModel : BindableBase
    {

        /// <value>Kolekcja produktów wyszukanych.</value>
        private ObservableCollection<ProductDto> _product;
        public ObservableCollection<ProductDto> Products
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        /// <value>Kolekcja produktów subskrybowanych.</value>
        private ObservableCollection<Product> _subscribeProductCollection;
        public ObservableCollection<Product> SubscribeProductCollection
        {
            get { return _subscribeProductCollection; }
            set { SetProperty(ref _subscribeProductCollection, value); }
        }

        /// <value>Tekst z pola szukania.</value>
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        readonly IProductRepository _productRepository;

        // Definicja własciwości DelegateCommand dla przycisków
        public DelegateCommand SearchProductCommand { get; private set; }
        public DelegateCommand<string> GoToWebSiteProductCommand { get; private set; }
        public DelegateCommand<ProductDto> SubscribeProductCommand { get; private set; }
        public DelegateCommand<string> UnSubscribeProductCommand { get; private set; }

        /// <summary>
        /// Konstruktor przypisujący wstrzykniete zależności oraz tworzący instancję dla DelegateCommand.
        /// </summary>
        /// <param name="productRepository">Wstrzykniete produkt repozytory</param>
        public MainWindowViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            SearchProductCommand = new DelegateCommand(SearchProductAsync, CanSearchProduct);
            GoToWebSiteProductCommand = new DelegateCommand<string>(GoToWebSiteProduct, CanGoToWebSiteProduct);
            SubscribeProductCommand = new DelegateCommand<ProductDto>(SubscribeProduct, CanSubscribeProduct);
            UnSubscribeProductCommand = new DelegateCommand<string>(UnSubscribeProduct, CanUnSubscribeProduct);
            GetSubscribeProduct();
        }

        /// <summary>
        /// Metoda wykonywana podczas wywołania komendy SearchProductCommand, pobierająca kolekcję produktów po nazwie zapisanej w _text i zapisującą w zmienej Produkts.
        /// </summary>
        void SearchProductAsync()
        {
            if (!string.IsNullOrWhiteSpace(_text))
            {
                var products = new ObservableCollection<ProductDto>(Task.Run(() => _productRepository.GetProductsAsync(_text)).Result);
                Products = products;
            }
        }

        /// <summary>
        /// Metoda sprawdzająca, czy można wykonać metodę SearchProductAsync.
        /// </summary>
        /// <returns>Wartość bool</returns>
        bool CanSearchProduct()
        {
            return true;
        }

        /// <summary>
        /// Metoda wykonywana podczas wywołania komendy GoToWebSiteProductCommand, otwierająca stronę produktu.
        /// </summary>
        /// <param name="id">id typu string</param>
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
        /// Metoda sprawdzająca, czy można wykonać metodę GoToWebSiteProduct
        /// </summary>
        /// <param name="id">Parametr typu string</param>
        /// <returns>Wartość bool</returns>
        bool CanGoToWebSiteProduct(string id)
        {
            return true;
        }

        /// <summary>
        /// Metoda wykonywana podczas wywołania komendy SubscribeProductCommand, wysyłająca zapytanie z produktem do za subskrybowania.
        /// </summary>
        /// <param name="productDto">productDto typu ProductDto</param>
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
                    Products.FirstOrDefault( p => p == productDto ).IsSubscribed = true;
                    Products = new ObservableCollection<ProductDto>(Products);
                    SubscribeProductCollection.Add(product);
                }
            }
        }

        /// <summary>
        /// Metoda sprawdzająca, czy można wykonać metodę SubscribeProduct.
        /// </summary>
        /// <param name="product">Parametr typu ProductDto</param>
        /// <returns>Wartość bool</returns>
        bool CanSubscribeProduct(ProductDto product)
        {
            return true;
        }

        /// <summary>
        /// Metoda wykonywana podczas wywołania komendy UnSubscribeProductCommand, wysyłająca zapytanie z id produktu do zniesienia subskrybcji.
        /// </summary>
        /// <param name="link"></param>
        void UnSubscribeProduct(string link)
        {
            if (!string.IsNullOrWhiteSpace(link))
            {
                var message = (Task.Run(() => _productRepository.UnSubscribeProductsAsync(link)).Result);
                if (message == "OK")
                {
                    var p = Products.FirstOrDefault(p => p.Link == link);
                    if ( p != null)
                    {
                        p.IsSubscribed = false;
                        Products = new ObservableCollection<ProductDto>(Products);
                    }
                    SubscribeProductCollection.Remove(SubscribeProductCollection.FirstOrDefault(p => p.Link == link));
   
                }
            }
        }

        /// <summary>
        /// Metoda sprawdzająca, czy można wykonać metodę UnSubscribeProduct.
        /// </summary>
        /// <param name="link">Parametr typu string</param>
        /// <returns>Wartość bool</returns>
        bool CanUnSubscribeProduct(string link)
        {
            return true;
        }

        /// <summary>
        /// Metoda pobierająca i wpisująca kolekcję zasubskrybowanych produków do pola SubscribeProductCollection.
        /// </summary>
        void GetSubscribeProduct()
        {
            SubscribeProductCollection = new ObservableCollection<Product>(Task.Run(() => _productRepository.GetSubscribeProductsAsync()).Result);
        }
    }
}
