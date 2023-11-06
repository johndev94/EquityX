using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.Model
{
    public class Stocks
    {
        public string company { get; set; }
        public string description { get; set; }
        public double initial_price { get; set; }
        public double price_2002 { get; set; }
        public double price_2007 { get; set; }
        public string symbol { get; set; }
    }
}
