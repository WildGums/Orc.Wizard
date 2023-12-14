namespace Orc.Wizard;

using System;
using System.Windows;
using System.Windows.Media;
using Catel.Logging;

internal static class FrameworkElementExtensions
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    public static SolidColorBrush GetAccentColorBrush(this FrameworkElement frameworkElement, bool isSelected = true)
    {
        ArgumentNullException.ThrowIfNull(frameworkElement);

        var resourceName = isSelected ? ThemingKeys.AccentColorBrush : ThemingKeys.AccentColorBrush40;

        if (frameworkElement.TryFindResource(resourceName) is not SolidColorBrush brush)
        {
            throw Log.ErrorAndCreateException<InvalidOperationException>("Theming is not yet initialized, make sure to initialize a theme via ThemeManager first");
        }

        return brush;
    }
}
