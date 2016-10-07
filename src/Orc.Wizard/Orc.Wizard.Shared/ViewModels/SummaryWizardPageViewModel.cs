// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SummaryWizardPageViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel.Collections;

    public class SummaryWizardPageViewModel : WizardPageViewModelBase<SummaryWizardPage>
    {
        public SummaryWizardPageViewModel(SummaryWizardPage wizardPage) 
            : base(wizardPage)
        {
            SummaryItems = new ObservableCollection<ISummaryItem>();
        }

        public ObservableCollection<ISummaryItem> SummaryItems { get; private set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var wizard = Wizard;
            if (wizard == null)
            {
                return;
            }

            foreach (var page in wizard.Pages)
            {
                var summary = page.GetSummary();
                if (summary != null)
                {
                    SummaryItems.Add(summary);
                }
            }
        }
    }
}