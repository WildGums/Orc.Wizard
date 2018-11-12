// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgeWizardPageViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.ViewModels
{
    using Catel.MVVM;

    public class AgeWizardPageViewModel : WizardPageViewModelBase<AgeWizardPage>
    {
        public AgeWizardPageViewModel(AgeWizardPage wizardPage)
            : base(wizardPage)
        {
        }

        [ViewModelToModel]
        public string Age { get; set; }
    }
}
