namespace Orc.Wizard.Tests;

using Catel.IoC;

public class TestWizard : WizardBase
{
    public TestWizard(ITypeFactory typeFactory)
        : base(typeFactory)
    {
    }
}
