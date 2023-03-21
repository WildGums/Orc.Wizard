namespace Orc.Wizard.Converters;

using System;
using System.Windows.Media;
using Catel.MVVM.Converters;

public class IsSelectedToBrushConverter : ValueConverterBase<bool>
{
    private static readonly Brush SelectedBrush = Brushes.Transparent;
    private static readonly Brush NotSelectedBrush = Brushes.Transparent;

    static IsSelectedToBrushConverter()
    {
        var application = System.Windows.Application.Current;
        if (application is not null)
        {
            SelectedBrush = application.FindResource(ThemingKeys.AccentColorBrush) as Brush ?? SelectedBrush;
            NotSelectedBrush = application.FindResource(ThemingKeys.AccentColorBrush40) as Brush ?? NotSelectedBrush;
        }
    }

    protected override object? Convert(bool value, Type targetType, object? parameter)
    {
        return value ? SelectedBrush : NotSelectedBrush;
    }
}