namespace Orc.Wizard;

using System;

public abstract class FullScreenWizardBase : WizardBase
{
    protected FullScreenWizardBase(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    public bool HideNavigationSystem { get; protected set; }
}
