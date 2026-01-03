namespace Orc.Wizard.Example
{
    using System;
    using System.Threading.Tasks;
    using Catel.Logging;
    using Catel.Reflection;
    using Catel.Services;
    using Microsoft.Extensions.Logging;
    using Orc.Wizard.ViewModels;
    using Orchestra.Windows;

    public class MonitorAwareWizardService : IMonitorAwareWizardService
    {
        private readonly ILogger<MonitorAwareWizardService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMonitorAwareUIVisualizerService _monitorAwareUiVisualizerService;

        public MonitorAwareWizardService(ILogger<MonitorAwareWizardService> logger,
            IServiceProvider serviceProvider, IMonitorAwareUIVisualizerService monitorAwareUiVisualizerService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _monitorAwareUiVisualizerService = monitorAwareUiVisualizerService;
        }

        public Task<UIVisualizerResult> ShowWizardAsync(IWizard wizard, IMonitorInfo monitor)
        {
            _logger.LogDebug("Showing wizard '{0}' on monitor '{1}'", wizard.GetType().GetSafeFullName(false), monitor);

            if (wizard is SideNavigationWizardBase)
            {
                return _monitorAwareUiVisualizerService.ShowDialogAsync<SideNavigationWizardViewModel>(_serviceProvider, wizard, monitor);
            }

            if (wizard is FullScreenWizardBase)
            {
                return _monitorAwareUiVisualizerService.ShowDialogAsync<FullScreenWizardViewModel>(_serviceProvider, wizard, monitor);
            }

            return _monitorAwareUiVisualizerService.ShowDialogAsync<WizardViewModel>(_serviceProvider, wizard, monitor);
        }
    }
}
