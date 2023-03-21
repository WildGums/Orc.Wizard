namespace Orc.Wizard.Example.Wizard;

public interface IExampleWizard : IWizard
{
    bool AllowQuickNavigationWrapper { get; set; }
    bool HandleNavigationStatesWrapper { get; set; }
    INavigationController NavigationControllerWrapper { get; set; }
    bool ShowHelpWrapper { get; set; }
    bool ShowInTaskbarWrapper { get; set; }
    bool CacheViewsWrapper { get; set; }
    bool AutoSizeSideNavigationPaneWrapper { get; set; }
}