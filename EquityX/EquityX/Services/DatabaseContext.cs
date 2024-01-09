using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Org.BouncyCastle.Crypto.Generators;
using EquityX.Model;
using EquityX.ViewModel;



namespace EquityX.Services
{
    public class DatabaseContext
    {
        static SQLiteAsyncConnection db;

        
        static async Task Init()
        {
            if (db != null)
                return;
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);
            try
            {
                await db.CreateTableAsync<User>();
                //seem to have issues here
                await db.CreateTableAsync<Stock>();
                
                await db.CreateTableAsync<Crypto>();
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<List<Result>> GetStocks(string stocks)
        {
            // Using due to API calls ruuning out
             Result test =new Result
            {
                language = "en-US",
                region = "US",
                quoteType = "CRYPTOCURRENCY",
                triggerable = true,
                quoteSourceName = "CryptoCurrency",
                currency = "USD",
                fullExchangeName = "CCC",
                longName = "Cardano USD",
                financialCurrency = "USD",
                averageDailyVolume3Month = 512512819,
                averageDailyVolume10Day = 577220045,
                fiftyTwoWeekLowChange = 0.29292476,
                fiftyTwoWeekLowChangePercent = 1.2712646,
                fiftyTwoWeekRange = "0.23042 - 0.677805",
                fiftyTwoWeekHighChange = -0.15446025,
                fiftyTwoWeekHighChangePercent = -0.22788303,
                fiftyTwoWeekLow = 0.23042,
                fiftyTwoWeekHigh = 0.677805,
                marketState = "REGULAR",
                shortName = "Cardano USD",
                exchangeDataDelayedBy = 0,
                regularMarketPrice = 0.52334476,
                regularMarketTime = 1704802800,
                regularMarketChange = 0.013881981,
                regularMarketOpen = 0.54107237,
                regularMarketDayHigh = 0.5414418,
                regularMarketDayLow = 0.51665485,
                regularMarketVolume = 584919936,
                priceHint = 4,
                symbol = "ADA-USD",
                
            };
            await Init();
            WebDataManager webDataManager = new WebDataManager();
            List<Result> resultList = await webDataManager.GetStock(stocks);

            // Ensure resultList is not null
            resultList ??= new List<Result>();

            // Add the test Result object to the resultList
            resultList.Add(test);

            return resultList;
        }
        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            await Init();
            
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Either email or password is null or empty.
                return false;
            }

            if (db == null)
            {
                // Database context is null.
                throw new InvalidOperationException("Database context is not initialized.");
            }

            try
            {
                var normalizedEmail = email.ToLower();

                var user = await db.Table<User>().FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);
                if (user != null && user.Password == password)
                {
                    return true; // Credentials match
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately.
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return false; // User not found, password doesn't match, or an exception occurred.
        }

        //add funds
        public async Task<bool> UpdateUserBalance(int userId, double addedFunds)
        {
            await Init();
            try
            {
                var user = await db.Table<User>().FirstOrDefaultAsync(u => u.UserId == userId);
                if (user != null)
                {
                    user.Balance += addedFunds;
                    await db.UpdateAsync(user);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user balance: {ex.Message}");
                return false;
            }
        }

        // Get balance with id
        public async Task<double> GetBalanceByUserId(int userId)
        {
            await Init(); // Ensure the database is initialized

            var user = await db.Table<User>().FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                return (double)user.Balance; // Return the user's balance
            }
            else
            {
                throw new InvalidOperationException("User not found.");
            }
        }

        // Add user with User
        public static async Task AddUserAsync(User user)
        {
            await Init();
            await db.InsertAsync(user);
        }
        // Add user with variables
        public async Task AddUserAsync(string email, string password)
        {
            await Init();

            var user = new User
            {
                Email = email,
                Password = HashPassword(password) 
            };

            await db.InsertAsync(user);
        }
        // Get all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            await Init();
            return await db.Table<User>().ToListAsync();
        }

        public async Task<int?> GetUserIdByEmail(string email)
        {
            await Init();
            var user = await db.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
            return user?.UserId; // Return the UserId if the user is found, otherwise null
        }

        // Remove a user by UserId
        public async Task RemoveUserAsync(int userId)
        {
            await Init();
            var user = await db.Table<User>().Where(u => u.UserId == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                await db.DeleteAsync(user);
            }
        }



        // Hashes password
        private string HashPassword(string password)
        {

            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        public async Task<User> GetUserAsync(int userId)
        {
            await Init();
            return await db.Table<User>().Where(u => u.UserId == userId).FirstOrDefaultAsync();
        }



        // Add stock
        public async Task AddStockAsync(Stock stock)
        {
            await Init();
            await db.InsertAsync(stock);
        }

        // Remove a stock by StockId
        public async Task RemoveStockAsync(int stockId)
        {
            await Init();
            var stock = await db.Table<Stock>().Where(s => s.StockId == stockId).FirstOrDefaultAsync();
            if (stock != null)
            {
                await db.DeleteAsync(stock);
            }
        }

        // Method to get all stocks
        public async Task<List<Stock>> GetAllStocksAsync()
        {
            await Init();
            return await db.Table<Stock>().ToListAsync();
        }



        // Add crypto
        public async Task AddCryptoAsync(Crypto crypto)
        {
            await Init();
            await db.InsertAsync(crypto);
        }

        // Remove a crypto entry by CryptoId
        public async Task RemoveCryptoAsync(int cryptoId)
        {
            await Init();
            var crypto = await db.Table<Crypto>().Where(c => c.CryptoId == cryptoId).FirstOrDefaultAsync();
            if (crypto != null)
            {
                await db.DeleteAsync(crypto);
            }
        }

        //NEED TO CREATE A GET ALL STOCKS/CRYPTOS BY USER ID


    }
}
