using CommunityToolkit.Mvvm.Messaging;
using EquityX.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.ViewModel
{
    public class BuyStockViewModel : INotifyPropertyChanged
    {
        private Model.Result _stock;
        public Model.Result Stock2
        {
            get => _stock;
            set
            {
                _stock = value;
                OnPropertyChanged(nameof(Stock2));
            }
        }

      public BuyStockViewModel()
        {
            Stock2 = new Model.Result();
        
            WeakReferenceMessenger.Default.Register<DataToBuyStockPage>(this, (r, m) =>
        {
            Stock2 = m.Value;
        });
            
        }


         public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
