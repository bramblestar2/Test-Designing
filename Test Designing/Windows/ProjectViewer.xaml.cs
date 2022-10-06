using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Test_Designing.MVVM.View.ProjectViewer;

namespace Test_Designing.Windows
{
    /// <summary>
    /// Interaction logic for ProjectViewer.xaml
    /// </summary>
    public partial class ProjectViewer : Window
    {
        ClipView clipView = new ClipView();
        LaunchpadView launchpadView = new LaunchpadView();
        FilesView filesView = new FilesView();
        AudioView AudioView = new AudioView();

        public ProjectViewer(string projectName, string path)
        {
            InitializeComponent();
            this.Title = projectName;
            WindowTextBlock.Text = this.Title;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            filesView = new FilesView(path);

            ClipVM_.Content = clipView;
            LaunchpadVM_.Content = launchpadView;
            FilesVM_.Content = filesView;
            AudioVM_.Content = AudioView;
        }

        public ProjectViewer()
        {
            InitializeComponent();
            WindowTextBlock.Text = this.Title;
        }

        private void Drag_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
