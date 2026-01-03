namespace Orc.Wizard.Example
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Catel.Logging;
    using Microsoft.Extensions.Logging;
    using Orchestra.Windows;

    public static class WindowExtensions
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(WindowExtensions));

        public static void MoveToMonitor(this Window window, IMonitorInfo monitor)
        {
            ArgumentNullException.ThrowIfNull(window);
            ArgumentNullException.ThrowIfNull(monitor);

            Logger.LogDebug($"Moving window '{window.GetType().FullName}' to monitor '{monitor}'");

            if (window.IsLoaded)
            {
                UpdateWindowState(window, monitor);
            }
            else
            {
                RoutedEventHandler? handler = null;
                handler = new RoutedEventHandler((sender, e) =>
                {
                    UpdateWindowState(window, monitor);

                    window.Loaded -= handler;
                });

                window.Loaded += handler;
            }
        }

        private static void UpdateWindowState(Window window, IMonitorInfo screen)
        {
            // TODO: Consider ignoring if the window is already on the correct screen
            //var currentMonitor = window.GetMonitor();
            //var currentScreen = currentMonitor?.FindScreen();
            //if (currentScreen is not null && currentScreen.DeviceName == screen.DeviceName)
            //{
            //    return;
            //}

            Logger.LogDebug($"Updating state of window '{window.GetType().FullName}' for screen '{screen.DeviceName}'");

            var previousWindowState = window.WindowState;
            var previousWindowStartupLocation = window.WindowStartupLocation;
            var previousSizeToContent = window.SizeToContent;
            var previousResizeMode = window.ResizeMode;

            // Note: normal state is required in order to move
            window.SetCurrentValue(Window.WindowStateProperty, WindowState.Normal);
            window.SetCurrentValue(Window.SizeToContentProperty, SizeToContent.Manual);
            window.WindowStartupLocation = WindowStartupLocation.Manual;
            window.SetCurrentValue(Window.ResizeModeProperty, ResizeMode.CanResize);

            double left = screen.WorkingArea.X;
            double top = screen.WorkingArea.Y;

            var mainWindow = Application.Current.MainWindow;
            var mainScreenDpi = VisualTreeHelper.GetDpi(mainWindow);

            // Detect relative position to main monitor
            var primaryMonitor = MonitorInfo.GetPrimaryMonitor();

            bool isVertical = false;

            if (primaryMonitor is not null)
            {
                // Inside these checks orientation can be top-down (if screen are different sizes)
                // Try to check it with monitor areas, suppose the second screen begins on point where monitor area of the main screen ends if layout is horizontal
                if (screen.WorkingArea.X < primaryMonitor.WorkingArea.X)
                {
                    // Right to Left (reverse)
                    isVertical = screen.MonitorArea.X + screen.MonitorArea.Width != primaryMonitor.MonitorArea.X;
                }

                if (screen.WorkingArea.X >= primaryMonitor.WorkingArea.X)
                {
                    // Left to right (common)
                    isVertical = screen.MonitorArea.X != primaryMonitor.MonitorArea.X + primaryMonitor.MonitorArea.Width;
                }

                var width = Math.Min(window.Width, screen.WorkingArea.Width);
                var height = Math.Min(window.Height, screen.WorkingArea.Height);

                if (previousWindowState == WindowState.Maximized)
                {
                    width = screen.WorkingArea.Width;
                    height = screen.WorkingArea.Height;
                }

                if (previousWindowStartupLocation != WindowStartupLocation.Manual)
                {
                    var leftDelta = (screen.WorkingArea.Width - width) / 2;
                    var topDelta = (screen.WorkingArea.Height - height) / 2;

                    left += leftDelta;
                    top += topDelta;
                }

                window.SetCurrentValue(FrameworkElement.WidthProperty, width);
                window.SetCurrentValue(FrameworkElement.HeightProperty, height);
                window.SetCurrentValue(Window.LeftProperty, left);
                window.SetCurrentValue(Window.TopProperty, top);

                window.SetCurrentValue(Window.SizeToContentProperty, previousSizeToContent);
                window.SetCurrentValue(Window.ResizeModeProperty, previousResizeMode);
                window.SetCurrentValue(Window.WindowStateProperty, previousWindowState);
            }
        }
    }
}
