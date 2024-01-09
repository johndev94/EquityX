using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace EquityX.Model
{
    public class Stock
    {
        [PrimaryKey, AutoIncrement]
        public int StockId { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; } 

        // Foreign key
        public int UserId { get; set; }

    }
}
