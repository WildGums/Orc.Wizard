namespace Orc.Wizard.Example
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using Catel;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Reflection;
    using Catel.Services;
    using Orchestra.Windows;

    public class MonitorAwareUIVisualizerService : UIVisualizerService, IMonitorAwareUIVisualizerService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public MonitorAwareUIVisualizerService(IViewLocator viewLocator, IDispatcherService dispatcherService) 
            : base(viewLocator, dispatcherService)
        {
        }

        public virtual Task<UIVisualizerResult> ShowDialogAsync(IViewModel viewModel, IMonitorInfo monitor)
        {
            var viewModelType = viewModel.GetType();
            var viewModelTypeName = viewModelType.GetSafeFullName();

            RegisterViewForViewModelIfRequired(viewModelType);

            return ShowDialogAsync(viewModelTypeName, viewModel, monitor);
        }

        public virtual async Task<UIVisualizerResult> ShowDialogAsync(string name, object data, IMonitorInfo monitor)
        {
            Argument.IsNotNullOrWhitespace("name", name);
            ArgumentNullException.ThrowIfNull(monitor);

            EnsureViewIsRegistered(name);

            Log.Debug($"Showing modal window '{name}' on monitor '{monitor}'");

            var window = await CreateWindowAsync(new UIVisualizerContext
            {
                Name = name,
                Data = data,
            }) as Window;

            var context = new UIVisualizerContext
            {
                IsModal = false
            };

            if (window is not null)
            {
                window.MoveToMonitor(monitor);

                return await ShowWindowAsync(window, new UIVisualizerContext
                {
                    IsModal = true
                });
            }

            return new UIVisualizerResult(null, context, null);
        }
    }
}
