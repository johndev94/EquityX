using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.Model
{
    internal class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) 
            : base(options) { }

        public DbSet<Stock> stocks { get; set; }
    }
}
