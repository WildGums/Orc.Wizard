// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardPageToBrushConverter.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Converters
{
    using System;
    using System.Windows.Media;
    using Catel.MVVM.Converters;

    public class IsSelectedToBrushConverter : ValueConverterBase<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter)
        {
            return value ? SelectedBrush : NotSelectedBrush;
        }

        public Brush SelectedBrush { get; set; }

        public Brush NotSelectedBrush { get; set; }
    }
}