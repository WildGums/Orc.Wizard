namespace Orc.Wizard;

using System;
using System.Threading.Tasks;
using Catel.Logging;
using Catel.Reflection;
using Catel.Services;
using Microsoft.Extensions.Logging;
using ViewModels;

public class WizardService : IWizardService
{
    private readonly ILogger<WizardService> _logger;
    private readonly IUIVisualizerService _uiVisualizerService;

    public WizardService(ILogger<WizardService> logger, IUIVisualizerService uiVisualizerService)
    {
        _logger = logger;
        _uiVisualizerService = uiVisualizerService;
    }

    public virtual Task<UIVisualizerResult> ShowWizardAsync(IWizard wizard)
    {
        ArgumentNullException.ThrowIfNull(wizard);

        _logger.LogDebug("Showing wizard '{0}'", wizard.GetType().GetSafeFullName());

        if (wizard is SideNavigationWizardBase)
        {
            return _uiVisualizerService.ShowDialogAsync<SideNavigationWizardViewModel>(wizard);
        }

        if (wizard is FullScreenWizardBase)
        {
            return _uiVisualizerService.ShowDialogAsync<FullScreenWizardViewModel>(wizard);
        }

        return _uiVisualizerService.ShowDialogAsync<WizardViewModel>(wizard);
    }
}
