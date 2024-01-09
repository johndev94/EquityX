using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.Model
{
    public class AssetQuote
    {
        public string? Symbol { get; set; }
        public string? FullName { get; set; }
        public string? Exchange { get; set; }
        public double Price { get; set; }
        public DateTime QuoteDateTime { get; set; }

        // Other properties

    }
}
