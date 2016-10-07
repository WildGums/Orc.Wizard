// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWizardPage.cs" company="WildGums">
//   Copyright (c) 2013 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System;
    using System.Security.RightsManagement;
    using System.Threading.Tasks;
    using Catel.MVVM;

    public interface IWizardPage
    {
        ISummaryItem GetSummary();

        IWizard Wizard { get; set; }

        IViewModel ViewModel { get; set; }

        event EventHandler<ViewModelChangedEventArgs> ViewModelChanged;

        string Title { get; set; }

        string BreadcrumbTitle { get; set; }

        string Description { get; set; }

        int Number { get; set; }

        bool IsOptional { get; }

        Task CancelAsync();
        Task SaveAsync();
    }
}