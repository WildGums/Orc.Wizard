namespace Orc.Wizard.Example
{
    using System.Threading.Tasks;
    using Catel.Services;
    using Orchestra.Windows;

    public interface IMonitorAwareWizardService
    {
        Task<UIVisualizerResult> ShowWizardAsync(IWizard wizard, MonitorInfo monitor);
    }
}
