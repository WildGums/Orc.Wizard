// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard.Example.ViewModels
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Wizard.Models;

    public class MainViewModel : ViewModelBase
    {
        private readonly IWizardService _wizardService;

        public MainViewModel(IWizardService wizardService)
        {
            Argument.IsNotNull(() => wizardService);

            _wizardService = wizardService;

            ShowWizard = new TaskCommand(OnShowWizardExecuteAsync);

            Title = "Orc.Wizard example";
        }

        #region Commands
        public TaskCommand ShowWizard { get; private set; }

        private Task OnShowWizardExecuteAsync()
        {
            return _wizardService.ShowWizardAsync<ExampleWizard>();
        }
        #endregion
    }
}