namespace Orc.Wizard.Example
{
    using System;
    using System.Threading.Tasks;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Services;
    using Orchestra.Windows;

    public static class IMonitorAwareUIVisualizerServiceExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static Task<UIVisualizerResult> ShowDialogAsync<TViewModel>(this IMonitorAwareUIVisualizerService monitorAwareUiVisualizerService, MonitorInfo monitor)
            where TViewModel : IViewModel
        {
            return ShowDialogAsync<TViewModel>(monitorAwareUiVisualizerService, null, monitor);
        }

        public static Task<UIVisualizerResult> ShowDialogAsync<TViewModel>(this IMonitorAwareUIVisualizerService monitorAwareUiVisualizerService, object? model, MonitorInfo monitor)
            where TViewModel : IViewModel
        {
            ArgumentNullException.ThrowIfNull(monitorAwareUiVisualizerService);

#pragma warning disable IDISP001 // Dispose created.
            var serviceLocator = monitorAwareUiVisualizerService.GetServiceLocator();
#pragma warning restore IDISP001 // Dispose created.
            var viewModelFactory = serviceLocator.ResolveRequiredType<IViewModelFactory>();
            var vm = viewModelFactory.CreateRequiredViewModel(typeof(TViewModel), model, null);

            return monitorAwareUiVisualizerService.ShowDialogAsync(vm, monitor);
        }
    }
}
