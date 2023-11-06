using EquityX.ViewModel;

namespace EquityX
{
    public partial class MainPage : ContentPage 
    {

        public MainPage(StockViewModel viewModel)
        {
            InitializeComponent();

            // Set the binding context to the ViewModel
            BindingContext = viewModel;
            

        }

    }
}