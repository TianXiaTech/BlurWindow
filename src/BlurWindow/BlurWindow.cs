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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TianXiaTech
{
    public class BlurWindow : Window
    {
        public static DependencyProperty TitleForegroundProperty = DependencyProperty.Register("TitleForeground", typeof(SolidColorBrush), typeof(BlurWindow), new PropertyMetadata(Brushes.Black));
        public static DependencyProperty ControlBoxVisibilityProperty = DependencyProperty.Register("ControlBoxVisibility", typeof(Visibility), typeof(BlurWindow), new PropertyMetadata(Visibility.Visible));
        public static DependencyProperty IconVisibilityProperty = DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(BlurWindow), new PropertyMetadata(Visibility.Visible));
        public static DependencyProperty TitleVisibilityProperty = DependencyProperty.Register("TitleVisibility", typeof(Visibility), typeof(BlurWindow), new PropertyMetadata(Visibility.Visible));
        public static DependencyProperty IsEnableContextMenuProperty = DependencyProperty.Register("IsEnableContextMenu", typeof(bool), typeof(BlurWindow), new PropertyMetadata(true));
        public static DependencyProperty MinimizeVisibilityProperty = DependencyProperty.Register("MinimizeVisibility", typeof(Visibility), typeof(BlurWindow), new PropertyMetadata(Visibility.Visible));
        public static DependencyProperty MaximizeVisibilityProperty = DependencyProperty.Register("MaximizeVisibility", typeof(Visibility), typeof(BlurWindow), new PropertyMetadata(Visibility.Visible));
        public static DependencyProperty CloseVisibilityProperty = DependencyProperty.Register("CloseVisibility", typeof(Visibility), typeof(BlurWindow), new PropertyMetadata(Visibility.Visible));

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

        public Visibility ControlBoxVisibility
        {
            get
            {
                return (Visibility)GetValue(ControlBoxVisibilityProperty);
            }
            set
            {
                SetValue(ControlBoxVisibilityProperty, value);
            }
        }

        public Visibility IconVisibility
        {
            get
            {
                return (Visibility)GetValue(IconVisibilityProperty);
            }
            set
            {
                SetValue(IconVisibilityProperty, value);
            }
        }

        public Visibility TitleVisibility
        {
            get
            {
                return (Visibility)GetValue(TitleVisibilityProperty);
            }
            set
            {
                SetValue(TitleVisibilityProperty, value);
            }
        }

        public bool IsEnableContextMenu
        {
            get
            {
                return (bool)GetValue(IsEnableContextMenuProperty);
            }
            set
            {
                SetValue(IsEnableContextMenuProperty, value);
            }
        }

        public Visibility MinimizeVisibility
        {
            get
            {
                return (Visibility)GetValue(MinimizeVisibilityProperty);
            }
            set
            {
                SetValue(MinimizeVisibilityProperty, value);
            }
        }

        public Visibility MaximizeVisibility
        {
            get
            {
                return (Visibility)GetValue(MaximizeVisibilityProperty);
            }
            set
            {
                SetValue(MaximizeVisibilityProperty, value);
            }
        }

        public Visibility CloseVisibility
        {
            get
            {
                return (Visibility)GetValue(CloseVisibilityProperty);
            }
            set
            {
                SetValue(CloseVisibilityProperty, value);
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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource.FromHwnd(new WindowInteropHelper(this).Handle).AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (((msg == WindowHelper.WM_SYSTEMMENU) && (wParam.ToInt32() == WindowHelper.WP_SYSTEMMENU)) || msg == WindowHelper.WM_NCRBUTTONUP)
            {
                HwndSource hwndSource = HwndSource.FromHwnd(hwnd);
                var blurWindow = hwndSource.RootVisual as BlurWindow;

                if (blurWindow != null)
                {
                    handled = !blurWindow.IsEnableContextMenu;
                }
                else
                {
                    handled = false;
                }
            }

            return IntPtr.Zero;
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
