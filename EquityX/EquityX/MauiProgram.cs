using EquityX.Services;
using EquityX.ViewModel;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;

namespace EquityX
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register the StocksService as a singleton with the DI service
            builder.Services.AddSingleton<StockService>();
            // Register the ViewModel
            builder.Services.AddSingleton<StockViewModel>();
            // Register our main page
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}