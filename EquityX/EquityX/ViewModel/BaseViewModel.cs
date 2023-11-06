using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.ViewModel;

public class BaseViewModel : INotifyPropertyChanged
{
    bool _isBusy;
    string _pageTitle;

    public bool IsBusy
    {
        get => _isBusy;

        set
        {
            if (_isBusy == value)
            {
                return;
            }
            _isBusy = value;

            OnPropertyChanged();

            //OnPropertyChanged(nameof(IsBusy));
            //OnPropertyChanged(nameof(IsNotBusy));
        }
    }

    public string PageTitle
    {
        get => _pageTitle;
        set
        {
            if(_pageTitle == value)
            { return; }
            _pageTitle = value;
            OnPropertyChanged();
        }
    }

    public bool IsNotBusy => !IsBusy;


    // This is a delegate, a method that will handle the PropertyChanged event when raised when a property value changes
    // This is the event that .net maui uses to update the view when the  data in the viewModel changes
    public event PropertyChangedEventHandler PropertyChanged;


    public void OnPropertyChanged([CallerMemberName] string name = null)
    {
        //[CallerMemberName] is a runtime compiler service which looks up at runtime which property invoked the onPropertyChanged method 
        // and passes the name of the property
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

