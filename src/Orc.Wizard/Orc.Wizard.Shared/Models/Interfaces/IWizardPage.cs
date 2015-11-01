// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizardPage.cs" company="Wild Gums">
//   Copyright (c) 2013 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using Catel.MVVM;

    public interface IWizardPage
    {
        ISummaryItem GetSummary();

        IWizard Wizard { get; set; }

        IViewModel ViewModel { get; set; }

        event EventHandler<ViewModelChangedEventArgs> ViewModelChanged;

        string Title { get; set; }

        string Description { get; set; }
    }
}