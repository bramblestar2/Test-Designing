using Test_Designing.Core;

namespace Test_Designing.MVVM.ViewModel.ProjectViewer
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand LaunchpadViewCommand { get; set; }
        public RelayCommand AudioViewCommand { get; set; }
        public RelayCommand ClipViewCommand { get; set; }
        public RelayCommand FilesViewCommand { get; set; }

        public LaunchpadViewModel LaunchpadVM { get; set; }
        public AudioViewModel AudioVM { get; set; }
        public ClipViewModel ClipVM { get; set; }
        public FilesViewModel FilesVM { get; set; }

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
            FilesVM = new FilesViewModel();

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
            FilesViewCommand = new RelayCommand(o =>
            {
                CurrentView = FilesVM;
            });
        }
    }
}
