using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.Model
{

    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        //public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double? Balance { get; set; }
        public double? Portfolio { get; set; }
        //public List<Stock> Stocks { get; set; } = new List<Stock>();

    }
}
