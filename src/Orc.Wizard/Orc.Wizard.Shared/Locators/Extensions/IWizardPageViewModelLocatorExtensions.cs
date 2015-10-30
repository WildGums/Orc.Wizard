// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizardPageViewModelLocatorExtensions.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using Catel;
    using Catel.MVVM;

    public static class IWizardPageViewModelLocatorExtensions
    {
        public static void RegisterWizardPageViewModel<TWizardPage, TViewModel>(this IWizardPageViewModelLocator locator)
            where TWizardPage : IWizardPage
            where TViewModel : IViewModel
        {
            Argument.IsNotNull(() => locator);

            locator.RegisterWizardPageViewModel(typeof(TWizardPage), typeof(TViewModel));
        }

        public static Type ResolveWizardPageViewModel<TWizardPage>(this IWizardPageViewModelLocator locator)
            where TWizardPage : IWizardPage
        {
            Argument.IsNotNull(() => locator);

            return locator.ResolveWizardPageViewModel(typeof(TWizardPage));
        }
    }
}