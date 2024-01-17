using BlurWindow.Helpers;
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
        public static DependencyProperty ContentSpanProperty = DependencyProperty.Register("ContentSpan", typeof(bool), typeof(BlurWindow), new PropertyMetadata(false));
        public static DependencyProperty IsBlurBackgroundProperty = DependencyProperty.Register("IsBlurBackground", typeof(bool), typeof(BlurWindow), new PropertyMetadata(false, OnIsBlurBackgroundChanged));
        public static DependencyProperty AcrylicOpacityProperty = DependencyProperty.Register("AcrylicOpacity", typeof(byte), typeof(BlurWindow), new PropertyMetadata((byte)128, OnAcrylicOpacityChanged));

        private Window blurBackgroundWindow;

        static BlurWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BlurWindow), new FrameworkPropertyMetadata(typeof(BlurWindow)));
        }

        static void OnIsBlurBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var blurWindow = d as BlurWindow;
            if ((bool)e.NewValue == true)
            {
                blurWindow.ShowBlurBackground();
            }
            else
            {
                blurWindow.CloseBlurBackground();
            }
        }

        private static void OnAcrylicOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var blurWindow = d as BlurWindow;
            WindowHelper.BlurWindow(blurWindow, (byte)e.NewValue);
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

        public bool ContentSpan
        {
            get
            {
                return (bool)GetValue(ContentSpanProperty);
            }
            set
            {
                SetValue(ContentSpanProperty, value);
            }
        }

        public bool IsBlurBackground
        {
            get
            {
                return (bool)GetValue(IsBlurBackgroundProperty);
            }
            set
            {
                SetValue(IsBlurBackgroundProperty, value);
            }
        }

        public byte AcrylicOpacity
        {
            get
            {
                return (byte)GetValue(AcrylicOpacityProperty);
            }
            set
            {
                SetValue(AcrylicOpacityProperty, value);
            }
        }

        public BlurWindow()
        {
            InitializeCommands();
            this.Loaded += (a, b) =>
            {
                WindowHelper.BlurWindow(this);
            };
            this.Closing += (a, b) =>
            {
                CloseBlurBackground();
            };
            this.SizeChanged += (a, b) => 
            {
                if (blurBackgroundWindow == null)
                    return;
                blurBackgroundWindow.Width = b.NewSize.Width;
                blurBackgroundWindow.Height = b.NewSize.Height;
                this.Focus();
            };
            this.StateChanged += (a, b) => 
            {
                if (blurBackgroundWindow == null)
                    return;

                blurBackgroundWindow.WindowState = this.WindowState;

                if(blurBackgroundWindow.WindowState == WindowState.Maximized)
                {
                    blurBackgroundWindow.WindowState = WindowState.Normal;
                    blurBackgroundWindow.Height = this.Height;
                    blurBackgroundWindow.Width = this.Width;
                    blurBackgroundWindow.Left = 0;
                    blurBackgroundWindow.Top = 0;
                    BeginBlurBackgroundAnimation();
                }

                if(this.WindowState == WindowState.Normal)
                {
                    BeginBlurBackgroundAnimation();
                }

                this.Focus();
            };
            this.LocationChanged += (a, b) => 
            {
                if (blurBackgroundWindow == null)
                    return;

                blurBackgroundWindow.Left = this.Left;
                blurBackgroundWindow.Top = this.Top;
                this.Focus();
            };

            InitializeWindows1122H2Style();
        }

        private void InitializeWindows1122H2Style()
        {
            if (OsVersionHelper.IsGreaterThanWindows1122H2() == false)
                return;

            //set window style
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
        }

        private void InitializeCommands()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, CloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, MaximizeWindow, CanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, MinimizeWindow, CanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, RestoreWindow, CanResizeWindow));
        }

        internal async void ShowBlurBackground()
        {
            if(blurBackgroundWindow == null)
                blurBackgroundWindow = new Window();
            blurBackgroundWindow.WindowStyle = WindowStyle.None;
            blurBackgroundWindow.ResizeMode = ResizeMode.NoResize;
            blurBackgroundWindow.ShowInTaskbar = false;
            blurBackgroundWindow.Width = this.Width;
            blurBackgroundWindow.Height = this.Height;
            blurBackgroundWindow.Left = this.Left;
            blurBackgroundWindow.Top = this.Top;
            blurBackgroundWindow.Background = this.Background;
            this.Background = new SolidColorBrush() { Color = Colors.White, Opacity = 0 };
            blurBackgroundWindow.AllowsTransparency = true;
            blurBackgroundWindow.Show();
            await Task.Delay(200);
            this.Focus();
        }

        internal void CloseBlurBackground()
        {
            if(this.blurBackgroundWindow != null)
            {
                this.Background = blurBackgroundWindow.Background;
                this.blurBackgroundWindow.Close();
                this.blurBackgroundWindow = null;
            }         
        }

        private void BeginBlurBackgroundAnimation()
        {
            blurBackgroundWindow.Opacity = 0;
            System.Windows.Media.Animation.DoubleAnimation doubleAnimation = new System.Windows.Media.Animation.DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 1;
            doubleAnimation.Duration = TimeSpan.FromMilliseconds(500);
            blurBackgroundWindow.BeginAnimation(OpacityProperty, doubleAnimation);
        }

        public void SetBackground(Brush brush)
        {
            if (this.blurBackgroundWindow != null)
            {
                this.blurBackgroundWindow.Background = brush;
            }
            else
            {
                this.Background = brush;
            }
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
