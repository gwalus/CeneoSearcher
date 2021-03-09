using DesktopClient.Interfaces;
using DesktopClient.Model;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<Product> _product;
        public ObservableCollection<Product> Products
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        readonly IProductRepository _productRepository;

        public DelegateCommand SearchProductCommand { get; private set; }

        public MainWindowViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            SearchProductCommand = new DelegateCommand(SearchProductAsync, CanSearchProduct);
        }

        void SearchProductAsync()
        {
            if (!string.IsNullOrWhiteSpace(_text))
            {
                var products = new ObservableCollection<Product>(Task.Run(() => _productRepository.GetProductsAsync(_text)).Result);
                foreach (var item in products)
                {
                    item.Image = item.Image.Insert(0, "http:");
                }
                Products = products;
            }
        }

        bool CanSearchProduct()
        {
            return true;
        }
    }
}
