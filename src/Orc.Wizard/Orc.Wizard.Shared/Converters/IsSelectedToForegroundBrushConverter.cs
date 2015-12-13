// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsSelectedToForegroundBrushConverter.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Converters
{
    using System;
    using System.Windows.Media;
    using Catel.MVVM.Converters;

    public class IsSelectedToForegroundBrushConverter : ValueConverterBase<bool>
    {
        private static readonly Brush SelectedBrush;
        private static readonly Brush NotSelectedBrush;

        static IsSelectedToForegroundBrushConverter()
        {
            var application = System.Windows.Application.Current;
            if (application != null)
            {
                SelectedBrush = Brushes.Black;
                NotSelectedBrush = Brushes.DimGray;
            }
        }

        protected override object Convert(bool value, Type targetType, object parameter)
        {
            return value ? SelectedBrush : NotSelectedBrush;
        }
    }
}