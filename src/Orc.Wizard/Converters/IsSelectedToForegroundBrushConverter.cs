namespace Orc.Wizard.Converters
{
    using System;
    using System.Windows.Media;
    using Catel.MVVM.Converters;

    public class IsSelectedToForegroundBrushConverter : ValueConverterBase<bool>
    {
        private static readonly Brush SelectedBrush = Brushes.Black;
        private static readonly Brush NotSelectedBrush = Brushes.DimGray;

        static IsSelectedToForegroundBrushConverter()
        {
            var application = System.Windows.Application.Current;
            if (application is not null)
            {
                SelectedBrush = Brushes.Black;
                NotSelectedBrush = Brushes.DimGray;
            }
        }

        protected override object? Convert(bool value, Type targetType, object? parameter)
        {
            return value ? SelectedBrush : NotSelectedBrush;
        }
    }
}
