namespace Orc.Wizard.Example.ViewModels;

using System;
using System.Linq;
using System.Threading.Tasks;
using Catel.Collections;
using Catel.MVVM;
using Microsoft.Extensions.DependencyInjection;
using Orc.Wizard.Example.Wizard;
using Orchestra;

public class MainViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IWizardService _wizardService;
    private readonly IMonitorAwareWizardService _monitorAwareWizardService;
    private readonly IMainWindowService _mainWindowService;

    public MainViewModel(IServiceProvider serviceProvider, IWizardService wizardService,
        IMonitorAwareWizardService monitorAwareWizardService, IMainWindowService mainWindowService)
        : base(serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _wizardService = wizardService;
        _monitorAwareWizardService = monitorAwareWizardService;
        _mainWindowService = mainWindowService;

        ShowWizard = new TaskCommand<Type>(serviceProvider, OnShowWizardExecuteAsync);
        UseFastForwardNavigationController = true;
        AllowQuickNavigation = true;
        ShowSummaryPage = true;
        ShowHelp = true;
        HandleNavigationStates = true;
        MarkAllPagesAsVisited = false;
        CacheViews = true;
        AutoSizeSideNavigationPane = false;
        MoveToOtherScreen = true;

        Title = "Orc.Wizard example";
    }

    public bool ShowInTaskbar { get; set; }

    public bool ShowHelp { get; set; }

    public bool AllowQuickNavigation { get; set; }

    public bool UseFastForwardNavigationController { get; set; }

    public bool ShowSummaryPage { get; set; }

    public bool HandleNavigationStates { get; set; }

    public bool MarkAllPagesAsVisited { get; set; }

    public bool CacheViews { get; set; }

    public bool AutoSizeSideNavigationPane { get; set; }

    public bool MoveToOtherScreen { get; set; }

    #region Commands
    public TaskCommand<Type> ShowWizard { get; private set; }

    private async Task OnShowWizardExecuteAsync(Type wizardType)
    {
        var wizard = ActivatorUtilities.CreateInstance(_serviceProvider, wizardType) as IExampleWizard;

        wizard.ShowInTaskbarWrapper = ShowInTaskbar;
        wizard.ShowHelpWrapper = ShowHelp;
        wizard.AllowQuickNavigationWrapper = AllowQuickNavigation;
        wizard.HandleNavigationStatesWrapper = HandleNavigationStates;
        wizard.CacheViewsWrapper = CacheViews;
        wizard.AutoSizeSideNavigationPaneWrapper = AutoSizeSideNavigationPane;

        if (UseFastForwardNavigationController)
        {
            wizard.NavigationControllerWrapper = ActivatorUtilities.CreateInstance<FastForwardNavigationController>(_serviceProvider, wizard);
        }

        if (!ShowSummaryPage)
        {
            var lastPage = wizard.Pages.Last();
            wizard.RemovePage(lastPage);
        }

        if (MarkAllPagesAsVisited)
        {
            wizard.Pages.ForEach(x => x.IsVisited = true);
        }

        if (MoveToOtherScreen)
        {
            // Just here to reproduce a potential bug
            var monitors = Orchestra.Windows.MonitorInfo.GetAllMonitors();
            if (monitors.Length > 1)
            {
                var mainWindow = await _mainWindowService.GetMainWindowAsync();
                var currentMonitor = Orchestra.Windows.MonitorInfo.GetMonitorFromWindow(mainWindow);

                var otherMonitor = monitors.FirstOrDefault(x => x.DeviceName != currentMonitor.DeviceName);

                await _monitorAwareWizardService.ShowWizardAsync(wizard, otherMonitor);
                return;
            }
        }

        await _wizardService.ShowWizardAsync(wizard);
    }
    #endregion
}
