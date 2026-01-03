namespace Orc.Wizard.Example
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using Catel;
    using Catel.MVVM;
    using Catel.MVVM.Views;
    using Catel.Reflection;
    using Catel.Services;
    using Microsoft.Extensions.Logging;
    using Orchestra.Windows;

    public class MonitorAwareUIVisualizerService : UIVisualizerService, IMonitorAwareUIVisualizerService
    {
        private readonly ILogger<UIVisualizerService> _logger;

        public MonitorAwareUIVisualizerService(ILogger<UIVisualizerService> logger, 
            IViewLocator viewLocator, IViewFactory viewFactory, IDispatcherService dispatcherService,
            IViewModelFactory viewModelFactory) 
            : base(logger, viewLocator, viewFactory, dispatcherService, viewModelFactory)
        {
            _logger = logger;
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

            _logger.LogDebug($"Showing modal window '{name}' on monitor '{monitor}'");

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
