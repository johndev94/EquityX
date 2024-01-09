using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EquityX.Services;
using System.ComponentModel;
using System;
using EquityX.Model;
using static SQLite.SQLite3;
using EquityX.View;
using CommunityToolkit.Mvvm.Messaging;
using EquityX.Messages;
using Microsoft.EntityFrameworkCore;
using Result = EquityX.Model.Result;


namespace EquityX.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseContext _dbService;

        private bool _isBusy;
        private string _statusMessage;

        public string NewStockName { get; set; }
        public double NewStockPrice { get; set; }
        public int NewStockQuantity { get; set; }

        public ObservableCollection<User> Users { get; }
        public ObservableCollection<Model.Result> Stocks { get; set; }
        public ObservableCollection<Stock> Stock1 { get; set; }

        private ObservableCollection<Model.Result> myVar;

        public ObservableCollection<Model.Result> MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


        // User input properties
        public string NewUserEmail { get; set; }
        public string NewUserPassword { get; set; }

        // Commands
        public ICommand AddUserCommand { get; }
        public ICommand LoadUsersCommand { get; }
        public ICommand BuyCommand { get; private set; }
        public ICommand AddStockCommand { get; }
        public ICommand LoadStocksCommand { get; }
        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private Model.Result _stock;
        public Model.Result Stock
        {
            get => _stock;
            set
            {
                _stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
        private List<Stock> _stocksList;
        public List<Stock> StocksList
        {
            get => _stocksList;
            set
            {
                _stocksList = value;
                OnPropertyChanged(nameof(StocksList));
            }
        }
        public MainViewModel()
        {
            Routing.RegisterRoute("buyStockPage", typeof(BuyStockPage));
            _dbService = new DatabaseContext();
            LoadStocks();

            Users = new ObservableCollection<User>();
            Stocks = new ObservableCollection<Model.Result>();


            BuyCommand = new Command<Model.Result>(async (stock) => await ExecuteBuyCommand(stock));

            AddUserCommand = new Command(async () => await AddUserAsync(), () => !IsBusy);
            LoadUsersCommand = new Command(async () => await LoadUsersAsync(), () => !IsBusy);

            AddStockCommand = new Command(async () => await AddStockAsync(), () => !IsBusy);
            LoadStocksCommand = new Command(async () => await LoadStocksAsync(), () => !IsBusy);
            GetStocks();
        }

        private async void LoadStocks()
        {
            var user = UserSession.CurrentUserId;
            var dbContext = new DatabaseContext();
            //Stock1 = await dbContext.GetAllStocksAsync();

            
        }

        private async Task ExecuteBuyCommand(Model.Result stock)
        {
            if (stock == null) return;


            try
            {
                //stock = new Model.Result();
                //stock.shortName = "test short name 2";
                //stock.regularMarketPrice = 100; 

                
                await Shell.Current.GoToAsync("buyStockPage");
                WeakReferenceMessenger.Default.Send(new DataToBuyStockPage(stock));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation error: {ex.Message}");
            }


        }
        public async Task GetStocks()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            Stocks.Clear();

            var stockList = await databaseContext.GetStocks("ADA-USD,BTC-USD,BUSD-USD,CEL-USD,CNTR-USD,COMET-USD");
            foreach (var stock in stockList)
            {
                var test = stock;
                Stocks.Add(stock); // Add each stock to the Stocks collection
            }
        }

        public async Task GetAllStocksAsync()
        {
            var dbContext = new DatabaseContext();
            var stockList = await dbContext.GetAllStocksAsync();
            var resultList = stockList.Select(stock => ConvertStockToResult(stock)).ToList();
            Stocks = new ObservableCollection<Result>(resultList);
        }

        private Result ConvertStockToResult(Stock stock)
        {
            return new Result
            { 
                longName = stock.Company,
                shortName = stock.Name,
                symbol = stock.Symbol,
                regularMarketPrice = stock.Price,

            };
        }
        public async Task AddUserAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                StatusMessage = string.Empty;

                await _dbService.AddUserAsync(NewUserEmail, NewUserPassword);
                StatusMessage = "User added successfully";
                await LoadUsersAsync(); 
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error adding user: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadUsersAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                Users.Clear();

                var users = await _dbService.GetAllUsersAsync();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading users: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddStockAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                StatusMessage = string.Empty;

                var stock = new Stock
                {
                    Name = NewStockName,
                    Price = NewStockPrice,
                    Quantity = NewStockQuantity
                };

                await _dbService.AddStockAsync(stock);
                StatusMessage = "Stock added successfully";
                await LoadStocksAsync(); // Reload the stock list
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error adding stock: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadStocksAsync()
        {
            if (IsBusy) return;

            try
            {
                Stocks.Clear();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading stocks: {ex.Message}";
            }
            finally
            {
               
            }
        }

    }
}
