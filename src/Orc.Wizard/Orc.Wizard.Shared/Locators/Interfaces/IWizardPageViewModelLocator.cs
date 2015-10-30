// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizardPageViewModelLocator.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;

    public interface IWizardPageViewModelLocator
    {
        void RegisterWizardPageViewModel(Type wizardPageType, Type viewModelType);
        Type ResolveWizardPageViewModel(Type wizardPageType);
    }
}