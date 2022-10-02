using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

namespace Test_Designing.MVVM.View.Settings
{
    /// <summary>
    /// Interaction logic for ThemeView.xaml
    /// </summary>
    public partial class ThemeView : UserControl
    {
        string dirLoc = System.AppDomain.CurrentDomain.BaseDirectory;
        public ThemeView()
        {
            InitializeComponent();
            Get_Settings();
        }

        private void Get_Settings()
        {
           if (File.Exists(dirLoc + "/Settings.json"))
           {
                JObject json = JObject.Parse((File.ReadAllText(dirLoc + "/Settings.json")));
                
                DarkThemeButton.IsChecked = ((bool)json["Themes"]["Dark"]);
                FlashbangThemeButton.IsChecked = ((bool)json["Themes"]["Flashbang"]);
           }
            else 
            {

                Root root = new Root()
                {
                    Themes = new Themes()
                    {
                        Dark = true,
                        Flashbang = false,
                    },
                };
                
                string json = JsonConvert.SerializeObject(root);
                File.WriteAllText(dirLoc + "/Settings.json", json);

                CustomMessageBox customMessageBox = new CustomMessageBox("Settings.json doesn't exist\nand poof, now it does");

                Get_Settings();
            }
        }

        private class Root
        {
            public Themes Themes { get; set; }
        }

        private class Themes
        {
            public bool Dark { get; set; }
            public bool Flashbang { get; set; }
        }
    }
}
