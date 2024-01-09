using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.Messages
{
    public class DataToBuyStockPage : ValueChangedMessage<Model.Result>
    {
        public DataToBuyStockPage(Model.Result value) : base(value) { }
    }
 
}
