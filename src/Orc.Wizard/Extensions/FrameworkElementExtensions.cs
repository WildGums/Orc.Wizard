namespace Orc.Wizard
{
    using System.Windows;
    using System.Windows.Media;
    using Catel;

    internal static class FrameworkElementExtensions
    {
        public static SolidColorBrush GetAccentColorBrush(this FrameworkElement frameworkElement, bool isSelected = true)
        {
            Argument.IsNotNull(() => frameworkElement);

            var resourceName = isSelected ? DefaultColorNames.AccentColorBrush : DefaultColorNames.AccentColorBrush4;

            var brush = frameworkElement.TryFindResource(resourceName) as SolidColorBrush;
            if (brush is null)
            {
                brush = new SolidColorBrush(isSelected ? DefaultColors.AccentColor : DefaultColors.AccentColor4);
            }

            return brush;
        }
    }
}
