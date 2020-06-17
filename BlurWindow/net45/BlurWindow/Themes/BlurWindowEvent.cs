using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BlurWindow.Themes
{
    public partial class BlurWindowEvent
    {
        private DateTime lastTime = DateTime.MinValue;
        private const int MouseDoubleClickTimeSpan = 500;

        public void Icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ts = DateTime.Now - lastTime;

            if(ts.TotalMilliseconds < MouseDoubleClickTimeSpan)
            {
                System.Windows.Application.Current.Shutdown(0);
            }
            else
            {
                var element = e.OriginalSource as FrameworkElement;
                if (element == null)
                    return;

                var window = FindAncestorWindow(element);

                var point = window.WindowState == WindowState.Maximized ? new Point(0, element.ActualHeight)
                    : new Point(window.Left + window.BorderThickness.Left, element.ActualHeight + window.Top + window.BorderThickness.Top);
                point = element.TransformToAncestor(window).Transform(point);
                SystemCommands.ShowSystemMenu(window, point);
            }

            lastTime = DateTime.Now;
        }

        private Window FindAncestorWindow(UIElement uIElement)
        {
            var window = LogicalTreeHelper.GetParent(uIElement);
            DependencyObject temp = null;

            while(window != null)
            {
                temp = window;
                window = VisualTreeHelper.GetParent(window);
            }

            return temp as Window;
        }
    }
}
