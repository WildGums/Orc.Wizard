namespace Orc.Wizard.Example.Wizard;

using System;
using System.Threading.Tasks;
using Catel;
using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class ExampleSideNavigationWizard : SideNavigationWizardBase, IExampleWizard
{
    private static readonly ILogger Logger = LogManager.GetLogger(typeof(ExampleSideNavigationWizard));

    private readonly IMessageService _messageService;

    public ExampleSideNavigationWizard(IServiceProvider serviceProvider, IMessageService messageService)
        : base(serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(messageService);

        _messageService = messageService;

        Title = "Orc.Wizard example"; 

        this.AddPage<PersonWizardPage>(serviceProvider);
        this.AddPage<AgeWizardPage>(serviceProvider);
        this.AddPage<Long1WizardPage>(serviceProvider);
        this.AddPage<Long2WizardPage>(serviceProvider);
        this.AddPage<SkillsWizardPage>(serviceProvider);
        this.AddPage<ComponentsWizardPage>(serviceProvider);
        this.AddPage<SummaryWizardPage>(serviceProvider);

        // Test for numbers being updated correctly
        this.InsertPage<GadgetsWizardPage>(serviceProvider, 4);

        MinSize = new System.Windows.Size(800, 600);
        MaxSize = new System.Windows.Size(1000, 800);

        ResizeMode = System.Windows.ResizeMode.CanResize;
    }

    public INavigationController NavigationControllerWrapper
    {
        get { return NavigationController; }
        set { NavigationController = value; }
    }

    public bool ShowInTaskbarWrapper
    {
        get {  return ShowInTaskbar; }
        set { ShowInTaskbar = value; }
    }

    public bool ShowHelpWrapper
    {
        get { return IsHelpVisible; }
        set { IsHelpVisible = value; }
    }

    public bool AllowQuickNavigationWrapper
    {
        get { return AllowQuickNavigation; }
        set { AllowQuickNavigation = value; }
    }

    public bool HandleNavigationStatesWrapper
    {
        get {  return HandleNavigationStates; }
        set { HandleNavigationStates = value; }
    }

    public bool CacheViewsWrapper
    {
        get { return CacheViews; }
        set { CacheViews = value; }
    }

    public bool AutoSizeSideNavigationPaneWrapper
    {
        get { return AutoSizeSideNavigationPane; }
        set { AutoSizeSideNavigationPane = value; }
    }

    public override Task ShowHelpAsync()
    {
        return _messageService.ShowAsync("HELP HANDLER");
    }

    public override async Task ResumeAsync()
    {
        Logger.LogInformation("Resuming wizard");

        await base.ResumeAsync();
    }       
}
