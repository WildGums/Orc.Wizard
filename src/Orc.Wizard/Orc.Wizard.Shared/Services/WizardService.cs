using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.Wizard.Services
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.Services;
    using ViewModels;

    public class WizardService : IWizardService
    {
        #region Fields
        private readonly IUIVisualizerService _uiVisualizerService;
        #endregion

        #region Constructors
        public WizardService(IUIVisualizerService uiVisualizerService)
        {
            Argument.IsNotNull(() => uiVisualizerService);

            _uiVisualizerService = uiVisualizerService;
        }
        #endregion

        #region Methods
        public async Task ShowWizardAsync(IWizard wizard)
        {
            await _uiVisualizerService.ShowDialogAsync<WizardViewModel>(wizard);
        }
        #endregion
    }
}
