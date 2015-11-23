// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AgeWizardPageViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.Wizard.ViewModels
{
    using Catel.MVVM;
    using Models;

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