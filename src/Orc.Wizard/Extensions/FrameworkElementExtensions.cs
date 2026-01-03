namespace Orc.Wizard;

using System;
using System.Windows;
using System.Windows.Media;
using Catel.Logging;

internal static class FrameworkElementExtensions
{
    public static SolidColorBrush GetAccentColorBrush(this FrameworkElement frameworkElement, bool isSelected = true)
    {
        ArgumentNullException.ThrowIfNull(frameworkElement);

        var resourceName = isSelected ? ThemingKeys.AccentColorBrush : ThemingKeys.AccentColorBrush40;

        if (frameworkElement.TryFindResource(resourceName) is not SolidColorBrush brush)
        {
            throw new InvalidOperationException("Theming is not yet initialized, make sure to initialize a theme via ThemeManager first");
        }

        return brush;
    }
}
