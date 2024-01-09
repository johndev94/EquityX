using EquityX.Services;
using EquityX.ViewModel;
using Microcharts.Maui;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using EquityX.Model;

namespace EquityX
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            Console.WriteLine(dbPath);
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
            builder.Services.AddDbContext<StockContext>(opt =>
                                opt.UseInMemoryDatabase("StockList"));

            builder.Services.AddSingleton<DatabaseContext>();
            builder.Services.AddSingleton<MainViewModel>();
            // Register the ViewModel
            //builder.Services.AddSingleton<StockViewModel>();
            // Register our main page
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}