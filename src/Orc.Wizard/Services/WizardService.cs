// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WizardService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Wizard
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using Catel.Reflection;
    using Catel.Services;
    using ViewModels;

    public class WizardService : IWizardService
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

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
        public Task<bool?> ShowWizardAsync(IWizard wizard)
        {
            Argument.IsNotNull(() => wizard);

            Log.Debug("Showing wizard '{0}'", wizard.GetType().GetSafeFullName(false));

            return _uiVisualizerService.ShowDialogAsync<WizardViewModel>(wizard);
        }
        #endregion
    }
}