// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardPageToBrushConverter.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Converters
{
    using System;
    using System.Windows.Media;
    using Catel.MVVM.Converters;

    public class IsSelectedToBrushConverter : ValueConverterBase<bool>
    {
        private static readonly Brush SelectedBrush;
        private static readonly Brush NotSelectedBrush;

        static IsSelectedToBrushConverter()
        {
            var application = System.Windows.Application.Current;
            if (application != null)
            {
                SelectedBrush = application.FindResource(DefaultColorNames.AccentColorBrush) as Brush ?? new SolidColorBrush(DefaultColors.AccentColor);
                NotSelectedBrush = application.FindResource(DefaultColorNames.AccentColorBrush4) as Brush ?? new SolidColorBrush(DefaultColors.AccentColor4);
            }
        }

        protected override object Convert(bool value, Type targetType, object parameter)
        {
            return value ? SelectedBrush : NotSelectedBrush;
        }
    }
}