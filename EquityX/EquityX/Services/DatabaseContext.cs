using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquityX.Model;
using SQLite;
using Org.BouncyCastle.Crypto.Generators;

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
            var user = await db.Table<User>().FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && user.Password == password)
            {
                return true; // Credentials match
            }
            return false; // User not found or password doesn't match
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
