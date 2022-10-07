using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Media;
using Test_Designing.Windows;
using static System.Net.Mime.MediaTypeNames;
using DragEventArgs = System.Windows.DragEventArgs;

namespace Test_Designing.MVVM.View.ProjectViewer
{
    /// <summary>
    /// Interaction logic for FilesView.xaml
    /// </summary>
    public partial class FilesView : UserControl
    {
        string[] files = null;
        string projectDir = null;
        public string selectedFile { get; }

        public FilesView(string dir)
        {
            InitializeComponent();
            projectDir = System.IO.Path.GetDirectoryName(dir);

            Get_Files();
            Add_Items();
        }

        public FilesView()
        {
            InitializeComponent();
        }

        private void Add_Items()
        {
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    ToolTip tool = new ToolTip();
                    tool.Style = (Style)FindResource("ToolTipTheme");

                    ItemsControl listView = new ItemsControl();
                    listView.Width = 60;
                    listView.Height = 80;
                    listView.Background = Brushes.Transparent;
                    listView.BorderBrush = new SolidColorBrush(Colors.Gray);
                    listView.BorderThickness = new Thickness(1, 1, 1, 1);
                    listView.IsHitTestVisible = true;
                    listView.ToolTip = tool;
                    listView.MouseDoubleClick += MouseDoubleClick;
                    listView.MouseDown += MouseDown;

                    TextBlock a = new TextBlock();
                    a.Text = System.IO.Path.GetFileName(files[i]);
                    a.TextWrapping = TextWrapping.Wrap;
                    a.Foreground = Brushes.White;
                    tool.Content = a.Text;

                    listView.Items.Add(a);
                    WrapPanel1.Items.Add(listView);
                }
            }
        }

        private void Get_Files()
        {
            if (Directory.Exists(projectDir + "/Files/"))
            {
                var temp = Directory.GetFiles(projectDir + "/Files/");

                //Filter the files
                for (int i = 0; i < temp.Length; i++)
                {
                    string ext = Path.GetExtension(temp[i]);

                    if ((ext == ".mp3" || ext == ".wav" || ext == ".ogg" || ext == ".aiff" || ext == ".flac"))
                    {
                        files = temp.Where(x =>
                        Path.GetExtension(x) == ".mp3" ||
                        Path.GetExtension(x) == ".wav" ||
                        Path.GetExtension(x) == ".ogg" ||
                        Path.GetExtension(x) == ".aiff" ||
                        Path.GetExtension(x) == ".flac").ToArray();
                    }
                }
            }
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            string[] fileLocations = (string[])e.Data.GetData(DataFormats.FileDrop);

            for (int i = 0; i < fileLocations.Length; i++)
            {
                if (!File.Exists(projectDir + "/Files/" + System.IO.Path.GetFileName(fileLocations[i])))
                {
                    string ext = Path.GetExtension(fileLocations[i]);
                    if ((ext == ".mp3" || ext == ".wav" || ext == ".ogg" || ext == ".aiff" || ext == ".flac"))
                    {
                        File.Copy(fileLocations[i], projectDir + "/Files/" + System.IO.Path.GetFileName(fileLocations[i]));

                        WrapPanel1.Items.Clear();
                        Get_Files();
                        Add_Items();
                    }
                }
                else
                {
                    CustomMessageBox customMessageBox = new CustomMessageBox("File already exists", "Error");
                }
            }
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ItemsControl && e.MiddleButton == MouseButtonState.Pressed)
            {
                ItemsControl a = (ItemsControl)sender;
                TextBlock itemsControl2 = (TextBlock)a.Items.GetItemAt(0);

                File.Delete(projectDir + "/Files/" + itemsControl2.Text);
                WrapPanel1.Items.Remove(a);
            }
        }

        private void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ItemsControl && e.LeftButton == MouseButtonState.Pressed)
            {
                ItemsControl itemsControl1 = (ItemsControl)sender;

                TextBlock itemsControl2 = (TextBlock)itemsControl1.Items.GetItemAt(0);

                ItemsControl itemsControl = new ItemsControl();
                itemsControl.Width = 60;
                itemsControl.Height = 80;
                itemsControl.Background = Brushes.Transparent;
                itemsControl.BorderBrush = new SolidColorBrush(Colors.Gray);
                itemsControl.BorderThickness = new Thickness(1, 1, 1, 1);
                itemsControl.IsHitTestVisible = true;
                itemsControl.ToolTip = itemsControl1.ToolTip;
                itemsControl.MouseDoubleClick += MouseDoubleClick; ;
                itemsControl.MouseDown += MouseDown;

                TextBlock a = new TextBlock();
                a.Text = itemsControl2.Text;
                a.TextWrapping = TextWrapping.Wrap;
                a.Foreground = Brushes.White;

                itemsControl.Items.Add(a);
                WrapPanel1.Items.Add(itemsControl);
                WrapPanel1.Items.Remove(itemsControl1);
            }
        }
    }
}
