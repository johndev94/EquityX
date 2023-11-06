using Microcharts;
using SkiaSharp;

namespace EquityX.View;

public partial class PortfolioPage : ContentPage
{
    ChartEntry[] entries = new[]
    {
        new ChartEntry(212)
        {
            Label = "Amazon",
            ValueLabel = "112",
            Color = SKColor.Parse("#2c3e50")
        },
        new ChartEntry(300)
        {
            Label = "Microsoft",
            ValueLabel = "300",
            Color = SKColor.Parse("#2c0050")
        },
        new ChartEntry(250)
        {
            Label = "Google",
            ValueLabel = "112",
            Color = SKColor.Parse("#2c1150")
        },
        new ChartEntry(112)
        {
            Label = "Tesla",
            ValueLabel = "222",
            Color = SKColor.Parse("#2c5550")
        }
    };
    public PortfolioPage()
	{
		InitializeComponent();

        chartView.Chart = new BarChart
        {
            Entries = entries
        };

        
    }
}