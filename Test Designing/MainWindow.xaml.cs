using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;

        public MainWindow()
        {
            InitializeComponent();
            WindowTextBlock.Text = this.Title;
            //Get_Project_Info();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Get_Project_Info()
        {
            //var lines = new List<string>(File.ReadAllLines(currentDir + "/ProjectInfos.txt").Where(arg => !string.IsNullOrWhiteSpace(arg)));
            if (File.Exists(currentDir + "/ProjectInfos.txt"))
                projects = File.ReadAllLines(currentDir + "/ProjectInfos.txt").Where(arg => !string.IsNullOrWhiteSpace(arg)).ToArray();
            else
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("ProjectInfos.txt doesn't exist\nand poof, now it does");

                File.Create(currentDir + "/ProjectInfos.txt").Close();

                Get_Project_Info();
            }

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
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListView1.SelectedIndex != -1)
            {
                string projectName;
                string projectPath;

                projectName = projects[ListView1.SelectedIndex * 2];
                projectPath = projects[ListView1.SelectedIndex];

                //if (File.Exists(projectPath))
                {
                    projectViewer = new ProjectViewer(projectName, projectPath);

                    this.Hide();
                    if (settings != null)
                        settings.Hide();

                    Nullable<bool> projectDialog = projectViewer.ShowDialog();

                    if (projectDialog == false)
                    {
                        this.Show();

                        if (settings != null)
                            settings.Hide();
                    }
                }
            }
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
                this.Hide();
                if (settings != null)
                    settings.Hide();

                string fileName = System.IO.Path.GetFileNameWithoutExtension(a.FileName);
                string fileFullName = System.IO.Path.GetFileName(a.FileName);
                string filePath = a.FileName;
                string fileDir = System.IO.Path.GetDirectoryName(filePath);
                string newDir = fileDir + "/" + fileName + "/";

                Directory.CreateDirectory(newDir);

                File.Create(newDir + fileFullName).Close();

                File.AppendAllText(currentDir + "/ProjectInfos.txt", "\n" + fileName);
                File.AppendAllText(currentDir + "/ProjectInfos.txt", "\n" + newDir + fileFullName);

                projectViewer = new ProjectViewer(fileName, filePath);
                Nullable<bool> projectDialog = projectViewer.ShowDialog();

                if (projectDialog == false)
                {
                    this.Show();

                    if (settings != null)
                        settings.Hide();
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
                this.Hide();
                if (settings != null)
                    settings.Hide();

                string fileName = System.IO.Path.GetFileNameWithoutExtension(a.FileName);
                string filePath = a.FileName;
                string fileFullName = System.IO.Path.GetFileName(a.FileName);
                string fileDir = System.IO.Path.GetDirectoryName(filePath);

                if (!Check_Existing_Path(filePath))
                {
                    File.AppendAllText(currentDir + "/ProjectInfos.txt", "\n" + fileName);
                    File.AppendAllText(currentDir + "/ProjectInfos.txt", "\n" + filePath);
                }

                projectViewer = new ProjectViewer(fileName, filePath);
                Nullable<bool> projectDialog = projectViewer.ShowDialog();

                if (projectDialog == false)
                {
                    this.Show();

                    if (settings != null)
                        settings.Hide();
                }
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (settings.WindowState == WindowState.Minimized)
                settings.WindowState = WindowState.Normal;
            else if (!settings.IsActive)
                settings.Show();
            
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ListView1.Items.Clear();
                Get_Project_Info();
            }
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                if (ListView1.SelectedIndex >= 0)
                {
                    //Delete lines for the selected project
                    var lines = new List<string>(File.ReadAllLines(currentDir + "/ProjectInfos.txt").Where(arg => !string.IsNullOrWhiteSpace(arg)));
                    lines.RemoveAt(ListView1.SelectedIndex);
                    lines.RemoveAt(ListView1.SelectedIndex);

                    File.WriteAllLines(currentDir + "/ProjectInfos.txt", lines.ToArray());

                    ListView1.Items.Clear();
                    Get_Project_Info();
                }
            }
        }

        private bool Check_Existing_Path(string path)
        {
            for (int i = 1; i < projects.Length; i+=2)
            {
                if (projects[i] == path)
                    return true;
            }

            return false;
        }
    }
}
