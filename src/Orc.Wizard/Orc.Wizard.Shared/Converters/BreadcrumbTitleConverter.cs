// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BreadcrumbTitleConverter.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Converters
{
    using System;
    using Catel.MVVM.Converters;

    public class BreadcrumbTitleConverter : ValueConverterBase<IWizardPage>
    {
        protected override object Convert(IWizardPage value, Type targetType, object parameter)
        {
            if (value == null)
            {
                return null;
            }

            var title = value.BreadcrumbTitle;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = value.Title;
            }

            return title;
        }
    }
}