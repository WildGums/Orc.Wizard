namespace Orc.Wizard.Tests;

using System;

public class NonCancellableTestWizard : WizardBase
{
    public NonCancellableTestWizard(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override bool CanCancel => false;
}
