namespace Orc.Wizard;

using System;

public abstract class SideNavigationWizardBase : WizardBase
{
    protected SideNavigationWizardBase(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    public bool ShowFullScreen { get; set; }
}
