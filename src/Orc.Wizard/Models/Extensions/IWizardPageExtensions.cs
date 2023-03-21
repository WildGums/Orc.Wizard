namespace Orc.Wizard;

using System;
using System.Threading.Tasks;

public static class IWizardPageExtensions
{
    public static Task MoveForwardOrResumeAsync(this IWizardPage wizardPage)
    {
        ArgumentNullException.ThrowIfNull(wizardPage);

        return wizardPage.Wizard?.MoveForwardOrResumeAsync() ?? Task.CompletedTask;
    }
}