using Prism.Mvvm;

namespace DesktopClient.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private string _text = "Welcome in prism library";

        public string Text
        {
            get { return _text; }
            set 
            {
                _text = value;
                SetProperty(ref _text, value);
            }
        }
    }
}
