namespace Orc.Wizard.Example
{
    using System.Threading.Tasks;
    using Catel.MVVM;
    using Catel.Services;
    using Orchestra.Windows;

    public interface IMonitorAwareUIVisualizerService
    {
        Task<UIVisualizerResult> ShowDialogAsync(IViewModel viewModel, IMonitorInfo monitor);
    }
}
