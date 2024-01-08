
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace EquityX.Services
{

    public class Crypto
    {
        [PrimaryKey, AutoIncrement]
        public int CryptoId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; } 

        // Foreign key
        public int UserId { get; set; }

    }
}
