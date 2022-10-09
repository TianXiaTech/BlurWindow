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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlurWindowDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : TianXiaTech.BlurWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cbx_FullContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.titlegrid.Visibility = this.cbx_FullContent.SelectedIndex == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void cbx_IconVisibility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.IconVisibility = cbx_IconVisibility.SelectedIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void cbx_TitleVisibility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.TitleVisibility = cbx_TitleVisibility.SelectedIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void cbx_ControlBoxVisibility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ControlBoxVisibility = cbx_ControlBoxVisibility.SelectedIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void cbx_EnableContextMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.IsEnableContextMenu = cbx_EnableContextMenu.SelectedIndex == 1 ? true : false;
        }

        private void cbx_TitleForeground_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.TitleForeground = cbx_TitleForeground.SelectedIndex == 1 ? Brushes.Black : Brushes.Blue;
        }
    }
}
