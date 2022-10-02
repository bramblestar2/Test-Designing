using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Designing.Core;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace Test_Designing.MVVM.ViewModel.Settings
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand ThemeViewCommand { get; set; }
        public ThemeViewModel ThemeVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            ThemeVM = new ThemeViewModel();
            CurrentView = ThemeVM;

            ThemeViewCommand = new RelayCommand(o => { CurrentView = ThemeVM; });
        }
    }
}
