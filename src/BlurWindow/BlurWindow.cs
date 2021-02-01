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

namespace TianXiaTech
{
    public class BlurWindow : Window
    {
        public static DependencyProperty TitleForegroundProperty = DependencyProperty.Register("TitlTitleForegrounde", typeof(SolidColorBrush), typeof(BlurWindow), new PropertyMetadata(Brushes.Black));

        static BlurWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BlurWindow), new FrameworkPropertyMetadata(typeof(BlurWindow)));
        }

        public SolidColorBrush TitleForeground
        {
            get
            {
                return (SolidColorBrush)GetValue(TitleForegroundProperty);
            }
            set
            {
                SetValue(TitleForegroundProperty, value);
            }
        }

        public BlurWindow()
        {
            InitializeCommands();
            this.Loaded += (a, b) => WindowHelper.BlurWindow(this);
        }

        private void InitializeCommands()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, CloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, MaximizeWindow, CanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, MinimizeWindow, CanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, RestoreWindow, CanResizeWindow));
        }

        private void CanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void CanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode != ResizeMode.NoResize;
        }

        private void CloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
            //SystemCommands.CloseWindow(this);
        }

        private void MaximizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void MinimizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void RestoreWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }
    }
}
