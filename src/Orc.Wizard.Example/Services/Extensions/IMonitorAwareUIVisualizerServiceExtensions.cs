namespace Orc.Wizard.Example
{
    using System;
    using System.Threading.Tasks;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Orchestra.Windows;

    public static class IMonitorAwareUIVisualizerServiceExtensions
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(IMonitorAwareUIVisualizerServiceExtensions));

        public static Task<UIVisualizerResult> ShowDialogAsync<TViewModel>(this IMonitorAwareUIVisualizerService monitorAwareUiVisualizerService,
            IServiceProvider serviceProvider, IMonitorInfo monitor)
            where TViewModel : IViewModel
        {
            return ShowDialogAsync<TViewModel>(monitorAwareUiVisualizerService, serviceProvider, null, monitor);
        }

        public static Task<UIVisualizerResult> ShowDialogAsync<TViewModel>(this IMonitorAwareUIVisualizerService monitorAwareUiVisualizerService, 
            IServiceProvider serviceProvider, object? model, IMonitorInfo monitor)
            where TViewModel : IViewModel
        {
            ArgumentNullException.ThrowIfNull(monitorAwareUiVisualizerService);

            var viewModelFactory = serviceProvider.GetRequiredService<IViewModelFactory>();
            var vm = viewModelFactory.CreateRequiredViewModel(typeof(TViewModel), model);

            return monitorAwareUiVisualizerService.ShowDialogAsync(vm, monitor);
        }
    }
}
