using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Org.BouncyCastle.Crypto.Generators;

namespace EquityX.Services
{
    public class DatabaseContext
    {
        static SQLiteAsyncConnection db;

        //public DatabaseContext()
        //{
        //    string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YourDatabase.db");
        //    db = new SQLiteAsyncConnection(databasePath);
        //}
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
