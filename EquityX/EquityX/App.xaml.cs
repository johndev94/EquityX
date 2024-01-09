using EquityX.Services;
using EquityX.View;

namespace EquityX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await UserSession.LoadSessionAsync();
        }
    }
}
