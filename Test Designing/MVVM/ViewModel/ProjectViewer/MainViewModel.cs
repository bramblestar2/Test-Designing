using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Designing.Core;
using Test_Designing.MVVM.ViewModel.Settings;

namespace Test_Designing.MVVM.ViewModel.ProjectViewer
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand LaunchpadViewCommand { get; set; }
        public RelayCommand AudioViewCommand { get; set; }
        public RelayCommand ClipViewCommand { get; set; }

        public LaunchpadViewModel LaunchpadVM { get; set; }
        public AudioViewModel AudioVM { get; set; }
        public ClipViewModel ClipVM { get; set; }

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
            LaunchpadVM = new LaunchpadViewModel();
            AudioVM = new AudioViewModel();
            ClipVM = new ClipViewModel();

            CurrentView = LaunchpadVM;

            LaunchpadViewCommand = new RelayCommand(o =>
            {
                CurrentView = LaunchpadVM;
            });
            AudioViewCommand = new RelayCommand(o =>
            {
                CurrentView = AudioVM;
            });
            ClipViewCommand = new RelayCommand(o =>
            {
                CurrentView = ClipVM;
            });
        }
    }
}
