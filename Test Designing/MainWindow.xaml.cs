using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test_Designing.Windows;

namespace Test_Designing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProjectViewer projectViewer;
        Settings settings = new Settings();
        string[] projects;

        public MainWindow()
        {
            InitializeComponent();
            WindowTextBlock.Text = this.Title;
            Get_Project_Info();
        }

        private void Get_Project_Info()
        {
            projects = File.ReadAllLines(System.AppDomain.CurrentDomain.BaseDirectory + "/ProjectInfos.txt");

            for (int i = 0; i < projects.Length; i++)
                Add_Project_Info(projects[i], projects[++i]);
        }

        private void Drag_Window(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            if (projectViewer!= null)
                settings.Close();

            if (projectViewer != null)
                projectViewer.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView1.SelectedIndex != -1)
            {
                string projectName;
                string projectPath;

                if (ListView1.SelectedIndex % 2 == 0)
                {
                    projectName = projects[ListView1.SelectedIndex];
                    projectPath = projects[ListView1.SelectedIndex + 1];
                }
                else
                {
                    projectName = projects[ListView1.SelectedIndex+1];
                    projectPath = projects[ListView1.SelectedIndex];
                }

                projectViewer = new ProjectViewer(projectName, projectPath);

                this.Close();

                Nullable<bool> projectDialog = projectViewer.ShowDialog();

                if (projectDialog == false)
                {
                    this.Close();
                    settings.Close();
                }
            }
        }

        private void Add_Project_Info(string projectName, string path)
        {
            ListView listView = new ListView();
            Thickness a = new Thickness(0,0,0,0);

            listView.BorderThickness = a;
            listView.Background = Brushes.Transparent;
            listView.IsHitTestVisible = false;

            TextBlock textBlock = new TextBlock();
            textBlock.Text = projectName;
            textBlock.FontSize = 20;
            textBlock.Foreground = Brushes.White;
            textBlock.TextWrapping = TextWrapping.Wrap;

            TextBlock textBlock1 = new TextBlock();
            textBlock1.Text = path;
            textBlock1.FontSize = 8;
            textBlock1.Foreground = Brushes.Gray;
            textBlock1.TextWrapping = TextWrapping.Wrap;

            listView.Items.Add(textBlock);
            listView.Items.Add(textBlock1);

            ListView1.Items.Add(listView);
        }

        private void New_Project_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog a = new SaveFileDialog();
            a.FileName = "Project";
            a.DefaultExt = ".drp";
            a.Filter = "DrumRack Project (.drp)|*.drp";
            Nullable<bool> result = a.ShowDialog();

            if (result == true)
            {
                this.Close();

                projectViewer = new ProjectViewer();
                Nullable<bool> projectDialog = projectViewer.ShowDialog();

                if (projectDialog == false)
                {
                    this.Close();
                    settings.Close();
                }
            }
        }

        private void Open_Project_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog a = new OpenFileDialog();
            a.DefaultExt = ".drp";
            a.Filter = "DrumRack Project (.drp)|*.drp";
            Nullable<bool> result = a.ShowDialog();

            if (result == true)
            {
                this.Close();

                projectViewer = new ProjectViewer();
                Nullable<bool> projectDialog = projectViewer.ShowDialog();

                if (projectDialog == false)
                {
                    this.Close();
                    settings.Close();
                }
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Nullable<bool> result = settings.ShowDialog();

            if (result == false)
            {
                settings.Close();
                settings = new Settings();
            }
        }
    }
}
