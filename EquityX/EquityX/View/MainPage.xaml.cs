using EquityX.Model;
using EquityX.ViewModel;



namespace EquityX
{
    public partial class MainPage : ContentPage 
    {
        WebDataManager resp = new WebDataManager();
        public MainPage()
        {
            InitializeComponent();
            resp.GetCrypto();
            BindingContext = new MainViewModel();
            
        }

    }
}