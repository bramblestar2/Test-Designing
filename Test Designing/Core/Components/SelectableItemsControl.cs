using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Test_Designing.Core.Components
{
    internal class SelectableItemsControl : ItemsControl
    {
        //-1 None selected
        public int selected_index { get; set; }

        SelectableItemsControl()
        {
            selected_index = -1;

            MouseDown += SelectableItemsControl_MouseDown;
        }

        private void SelectableItemsControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var btn = (dynamic)sender;
            for (int i = 0; i < Items.Count; i++)
            {
                if (FindName(btn.Name))
                {
                    selected_index = FindName(btn.Name);
                }
                else if (sender is ItemsControl)
                {
                    
                }
            }
        }
    }
}
