using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test_Designing.Windows
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string message, string title)
        {
            InitializeComponent();
            WindowTextBlock.Text = title;
            this.Title = title;
            CustomMessage.Text = message;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Show();
        }

        public CustomMessageBox(string message)
        {
            InitializeComponent();
            WindowTextBlock.Text = "";
            CustomMessage.Text = message;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Show();
        }

        public CustomMessageBox()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Show();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
