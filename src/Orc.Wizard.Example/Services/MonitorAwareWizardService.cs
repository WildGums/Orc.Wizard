namespace Orc.Wizard.Example
{
    using System;
    using System.Threading.Tasks;
    using Catel.Logging;
    using Catel.Reflection;
    using Catel.Services;
    using Orc.Wizard.ViewModels;
    using Orchestra.Windows;

    public class MonitorAwareWizardService : IMonitorAwareWizardService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IMonitorAwareUIVisualizerService _monitorAwareUiVisualizerService;

        public MonitorAwareWizardService(IMonitorAwareUIVisualizerService monitorAwareUiVisualizerService)
        {
            ArgumentNullException.ThrowIfNull(monitorAwareUiVisualizerService);

            _monitorAwareUiVisualizerService = monitorAwareUiVisualizerService;
        }

        public Task<UIVisualizerResult> ShowWizardAsync(IWizard wizard, IMonitorInfo monitor)
        {
            ArgumentNullException.ThrowIfNull(wizard);
            ArgumentNullException.ThrowIfNull(monitor);

            Log.Debug("Showing wizard '{0}' on monitor '{1}'", wizard.GetType().GetSafeFullName(false), monitor);

            if (wizard is SideNavigationWizardBase)
            {
                return _monitorAwareUiVisualizerService.ShowDialogAsync<SideNavigationWizardViewModel>(wizard, monitor);
            }

            if (wizard is FullScreenWizardBase)
            {
                return _monitorAwareUiVisualizerService.ShowDialogAsync<FullScreenWizardViewModel>(wizard, monitor);
            }

            return _monitorAwareUiVisualizerService.ShowDialogAsync<WizardViewModel>(wizard, monitor);
        }
    }
}
