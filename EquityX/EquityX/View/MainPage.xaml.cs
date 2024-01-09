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
            var testArray = resp.GetStock("COMET-USD");
            resp.GetStock("COMET-USD");
            BindingContext = new MainViewModel();
            
        }

    }
}